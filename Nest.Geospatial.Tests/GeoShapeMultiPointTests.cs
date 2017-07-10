using FluentAssertions;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using Xunit;

namespace Nest.Geospatial.Tests
{
    [Collection(TypeOfCluster.Geo)]
    public class GeoShapeMultiPointTests : GeoTests
    {
        public GeoShapeMultiPointTests(GeoCluster cluster) : base(cluster)
        {
        }

        [Fact]
        public void WithGeoShapeFilteredQuery()
        {
            var multiPoint = new MultiPoint(new IPoint[]
            {
                new Point(151.24397277832031, -33.828786509874291),
                new Point(151.241346, -33.843136)
            });

            var response = Client.Search<Suburb>(s => s
                .Query(q => q
                    .Filtered(fq => fq
                        .Query(fqf => fqf
                            .GeoShape(g => g
                                .OnField(f => f.Geometry)
                                .Coordinates(multiPoint)
                            )
                        )
                    )

                )
            );

            response.IsValid.Should().BeTrue();
            response.Total.Should().Be(2);
            response.Documents.Should().Contain(s => s.Name == "Mosman");
            response.Documents.Should().Contain(s => s.Name == "Unclassified NSW");
        }

        [Fact]
        public void WithGeoShapeFilteredFilter()
        {
            var multiPoint = new MultiPoint(new IPoint[]
            {
                new Point(151.24397277832031, -33.828786509874291),
                new Point(151.241346, -33.843136)
            });

            var response = Client.Search<Suburb>(s => s
                .Query(q => q
                    .Filtered(fq => fq
                        .Filter(fqf => fqf
                            .GeoShape(g => g
                                .OnField(f => f.Geometry)
                                .Coordinates(multiPoint)
                            )
                        )
                    )

                )
            );

            response.IsValid.Should().BeTrue();
            response.Total.Should().Be(2);
            response.Documents.Should().Contain(s => s.Name == "Mosman");
            response.Documents.Should().Contain(s => s.Name == "Unclassified NSW");
        }

        [Fact]
        public void WithGeoShapePointFilteredQuery()
        {
            var multiPoint = new MultiPoint(new IPoint[]
            {
                new Point(151.24397277832031, -33.828786509874291),
                new Point(151.241346, -33.843136)
            });

            var response = Client.Search<Suburb>(s => s
                .Query(q => q
                    .Filtered(fq => fq
                        .Query(fqf => fqf
                            .GeoShapeMultiPoint(g => g
                                .OnField(f => f.Geometry)
                                .Coordinates(multiPoint)
                            )
                        )					
                    )

                )
            );

            response.IsValid.Should().BeTrue();
            response.Total.Should().Be(2);
            response.Documents.Should().Contain(s => s.Name == "Mosman");
            response.Documents.Should().Contain(s => s.Name == "Unclassified NSW");
        }

        [Fact]
        public void WithGeoShapePointQuery()
        {
            var multiPoint = new MultiPoint(new IPoint[]
            {
                new Point(151.24397277832031, -33.828786509874291),
                new Point(151.241346, -33.843136)
            });

            var response = Client.Search<Suburb>(s => s
                .Query(q => q
                    .GeoShapeMultiPoint(g => g
                        .OnField(f => f.Geometry)
                        .Coordinates(multiPoint)
                    )
                )
            );

            response.IsValid.Should().BeTrue();
            response.Total.Should().Be(2);
            response.Documents.Should().Contain(s => s.Name == "Mosman");
            response.Documents.Should().Contain(s => s.Name == "Unclassified NSW");
        }
    }
}