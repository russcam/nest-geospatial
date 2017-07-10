using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NetTopologySuite.Geometries;
using Xunit;

namespace Nest.Geospatial.Tests
{
	[Collection(TypeOfCluster.Geo)]
	public class GeoShapePointQueryTests : GeoTests
	{
		public GeoShapePointQueryTests(GeoCluster cluster) : base(cluster)
		{
		}

		[Fact]
		public void GetSuburbFromPoint()
		{
			var point = new Point(151.24397277832031, -33.828786509874291);

			var response = Client.Search<Suburb>(s => s
				.Query(q => q
					.GeoShapePoint(g => g
						.OnField(f => f.Geometry)
						.Coordinates(point)
					)
				)
			);

			response.IsValid.Should().BeTrue();
			response.Total.Should().Be(1);
			response.Documents.First().Name.Should().Be("Mosman");
		}
	}

	[Collection(TypeOfCluster.Geo)]
	public class GeoShapePointFilterTests : GeoTests
	{
		public GeoShapePointFilterTests(GeoCluster cluster) : base(cluster)
		{
		}

		[Fact]
		public void WithGeoShape()
		{
			var point = new Point(151.24397277832031, -33.828786509874291);

			var response = Client.Search<Suburb>(s => s
				.Query(q => q
					.Filtered(fq => fq
						.Query(fqf => fqf
							.GeoShape(g => g
								.OnField(f => f.Geometry)
								.Coordinates(point)
							)
						)
					)

				)
			);

			response.IsValid.Should().BeTrue();
			response.Total.Should().Be(1);
			response.Documents.First().Name.Should().Be("Mosman");
		}

		[Fact]
		public void WithGeoShapePoint()
		{
			var point = new Point(151.24397277832031, -33.828786509874291);

			var response = Client.Search<Suburb>(s => s
				.Query(q => q
					.Filtered(fq => fq
						.Query(fqf => fqf
							.GeoShapePoint(g => g
								.OnField(f => f.Geometry)
								.Coordinates(point)
							)
						)					
					)

				)
			);

			response.IsValid.Should().BeTrue();
			response.Total.Should().Be(1);
			response.Documents.First().Name.Should().Be("Mosman");
		}
	}
}
