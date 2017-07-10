using System.Linq;
using FluentAssertions;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using Xunit;

namespace Nest.Geospatial.Tests
{
    [Collection(TypeOfCluster.Geo)]
    public class GeoShapePolygonTests : GeoTests
    {
        private readonly IGetResponse<Suburb> _mosmanSuburb;

        public GeoShapePolygonTests(GeoCluster cluster) : base(cluster)
        {
            var client = cluster.GetClient();
            _mosmanSuburb = client.Get<Suburb>(11681);
        }

        [Fact]
        public void WithFilteredGeoShapeQuery()
        {
            var response = Client.Search<Suburb>(s => s
                .Query(q => q
                    .Filtered(fq => fq
                        .Query(fqf => fqf
                            .GeoShape(g => g
                                .OnField(f => f.Geometry)
                                .Coordinates(_mosmanSuburb.Source.Geometry)
                            )
                        )
                    )

                )
            );

            response.IsValid.Should().BeTrue();
            response.Total.Should().Be(3);
            response.Documents.Should().Contain(s => s.Name == "Mosman");
            response.Documents.Should().Contain(s => s.Name == "Cremorne");
            response.Documents.Should().Contain(s => s.Name == "Unclassified NSW");
        }

        [Fact]
        public void WithFilteredGeoShapeFilter()
        {
            var response = Client.Search<Suburb>(s => s
                .Query(q => q
                    .Filtered(fq => fq
                        .Filter(fqf => fqf
                            .GeoShape(g => g
                                .OnField(f => f.Geometry)
                                .Coordinates(_mosmanSuburb.Source.Geometry)
                            )
                        )
                    )

                )
            );

            response.IsValid.Should().BeTrue();
            response.Total.Should().Be(3);
            response.Documents.Should().Contain(s => s.Name == "Mosman");
            response.Documents.Should().Contain(s => s.Name == "Cremorne");
            response.Documents.Should().Contain(s => s.Name == "Unclassified NSW");
        }

        [Fact]
        public void WithFilteredGeoShapePolygonQuery()
        {
            var response = Client.Search<Suburb>(s => s
                .Query(q => q
                    .Filtered(fq => fq
                        .Query(fqf => fqf
                            .GeoShapePolygon(g => g
                                .OnField(f => f.Geometry)
                                .Coordinates((IPolygon)_mosmanSuburb.Source.Geometry)
                            )
                        )					
                    )

                )
            );

            response.IsValid.Should().BeTrue();
            response.Total.Should().Be(3);
            response.Documents.Should().Contain(s => s.Name == "Mosman");
            response.Documents.Should().Contain(s => s.Name == "Cremorne");
            response.Documents.Should().Contain(s => s.Name == "Unclassified NSW");
        }

        [Fact]
        public void WithGeoShapePolygon()
        {
            var response = Client.Search<Suburb>(s => s
                .Query(q => q
                    .GeoShapePolygon(g => g
                        .OnField(f => f.Geometry)
                        .Coordinates((IPolygon)_mosmanSuburb.Source.Geometry)
                    )
                )
            );

            response.IsValid.Should().BeTrue();
            response.Total.Should().Be(3);
            response.Documents.Should().Contain(s => s.Name == "Mosman");
            response.Documents.Should().Contain(s => s.Name == "Cremorne");
            response.Documents.Should().Contain(s => s.Name == "Unclassified NSW");
        }
    }
}