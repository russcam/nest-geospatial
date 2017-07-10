using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json.Converters;
using Xunit;

namespace Nest.Geospatial.Tests
{
	[CollectionDefinition(TypeOfCluster.Geo)]
	public class GeoCluster : ClusterBase, ICollectionFixture<GeoCluster>
	{
		public const string SuburbsIndex = "suburbs";
		private readonly int BulkSize = 50;

		private string GeoData => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NEST", "GeoData");

		private string SuburbFolder => 
            Path.Combine(GeoData, "suburbs");

		public override void Boostrap()
		{
			DownloadSuburbData();
			IndexSuburbs();
		}

		public override IElasticClient GetClient(Func<ConnectionSettings, ConnectionSettings> settings = null)
		{
		    ConnectionSettings GeoSettings(ConnectionSettings s) => s
                .SetDefaultIndex(SuburbsIndex)
		        .SetJsonSerializerSettingsModifier(js =>
		        {
		            js.Converters.Add(new GeometryConverter());
		            js.Converters.Add(new CoordinateConverter());
		            js.Converters.Add(new StringEnumConverter());
		        });

		    settings = settings != null 
                ? (s => GeoSettings(settings(s))) 
                : (Func<ConnectionSettings,ConnectionSettings>) GeoSettings;

			return base.GetClient(settings);
		}

		private static void IndexSuburbs(IElasticClient client, IEnumerable<Suburb> suburbs)
		{
			var response = client.IndexMany(suburbs);
			if (!response.IsValid)
			{
				foreach (var item in response.ItemsWithErrors)
				{
					Console.WriteLine(item.Error);
				}
			}
		}

		private static DbaseFileHeader ReadShapefileFields(ShapefileDataReader reader)
		{
			var dbaseHeader = reader.DbaseHeader;
			Console.WriteLine("{0} Columns, {1} Records", dbaseHeader.Fields.Length, dbaseHeader.NumRecords);
			return dbaseHeader;
		}

		private void DownloadSuburbData()
		{
			var localZip = Path.Combine(GeoData, "suburbs.zip");
			var absUrl =
				"http://www.ausstats.abs.gov.au/ausstats/subscriber.nsf/0/2E96C5C5F3054EDFCA25731A002140DD/$File/2923030001ssc06aaust.zip";

			Directory.CreateDirectory(GeoData);

			if (!File.Exists(localZip))
			{
				Console.WriteLine($"Download State Suburbs from {absUrl}");
				new WebClient().DownloadFile(absUrl, localZip);
				Console.WriteLine("Downloaded State Suburbs");
			}

			if (!Directory.Exists(SuburbFolder))
			{
				Directory.CreateDirectory(SuburbFolder);
				Console.WriteLine("Unzipping State Suburbs...");
				ZipFile.ExtractToDirectory(localZip, SuburbFolder);
			}
		}

		private void IndexSuburbs()
		{
			var client = GetClient();

			if (client.IndexExists(SuburbsIndex).Exists)
			{
				return;
			}

			client.CreateIndex(SuburbsIndex, c => c
				.NumberOfShards(1)
				.NumberOfReplicas(0)
				.AddMapping<Suburb>(mm => mm
					.MapFromAttributes()
					.Properties(p => p
						.GeoShape(g => g
							.Name(n => n.Geometry)
							.Precision(10, GeoPrecisionUnit.Meters)
							.Tree(GeoTree.Quadtree)
						)
						.String(s => s
							.Name(n => n.State)
							.Index(FieldIndexOption.NotAnalyzed)
						)
					)
				)
			);

			var filename = Path.Combine(SuburbFolder, @"SSC06aAUST_region.shp");

			using (var reader = new ShapefileDataReader(filename, GeometryFactory.Default))
			{
				var dbaseHeader = ReadShapefileFields(reader);
				var suburbs = new List<Suburb>(BulkSize);
				var stopWatch = Stopwatch.StartNew();
				var count = 0;

				while (reader.Read())
				{
					count++;
					var attributes = new AttributesTable();

					for (var i = 0; i < dbaseHeader.NumFields; i++)
					{
						var fieldDescriptor = dbaseHeader.Fields[i];
						attributes.AddAttribute(fieldDescriptor.Name, reader.GetValue(i));
					}

					var name = attributes["NAME_2006"].ToString();
					var id = int.Parse(attributes["SSC_2006"].ToString());

					var suburb = new Suburb
					{
						Geometry = reader.Geometry,
						Name = name,
						State = (State)Enum.Parse(typeof(State), attributes["STATE_2006"].ToString(), true),
						Id = id
					};

					suburbs.Add(suburb);

					if (suburbs.Count == BulkSize)
					{
						IndexSuburbs(client, suburbs);
						Console.WriteLine("indexed {0} suburbs in {1}", count, stopWatch.Elapsed);
						suburbs.Clear();
					}
				}

				if (suburbs.Any())
				{
					IndexSuburbs(client, suburbs);
				}
				Console.WriteLine("indexed {0} suburbs in {1}", count, stopWatch.Elapsed);
			}
		}
	}
}