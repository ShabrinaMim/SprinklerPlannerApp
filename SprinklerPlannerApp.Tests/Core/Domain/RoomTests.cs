using Xunit;
using SprinklerPlannerApp.Core.Domain;
using System.Collections.Generic;

namespace SprinklerPlannerApp.Tests.Core.Domain
{
    public class RoomTests
    {
        private readonly List<Point3D> _squareRoom = new List<Point3D>
        {
            new Point3D(0, 0, 0),
            new Point3D(10, 0, 0),
            new Point3D(10, 10, 0),
            new Point3D(0, 10, 0)
        };

        [Fact]
        public void GetBounds_ReturnsCorrectMinMaxCoordinates()
        {
            Room room = new Room(_squareRoom);

            (double minX, double maxX, double minY, double maxY) = room.GetBounds();

            Assert.Equal(0, minX);
            Assert.Equal(10, maxX);
            Assert.Equal(0, minY);
            Assert.Equal(10, maxY);
        }

        [Theory]
        [InlineData(5, 5, true)]
        [InlineData(0, 0, true)]
        [InlineData(10, 0, false)]
        [InlineData(-1, -1, false)]
        public void IsInside_CheckVariousPoints(double x, double y, bool expected)
        {
            List<Point3D> corners = new List<Point3D>
            {
                new Point3D(0, 0, 0),
                new Point3D(10, 0, 0),
                new Point3D(10, 10, 0),
                new Point3D(0, 10, 0)
            };

            Room room = new Room(corners);
            Point3D testPoint = new Point3D(x, y, 0);

            bool result = room.IsInside(testPoint);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 5, 2, true)]
        [InlineData(0.5, 0.5, 2, false)]
        [InlineData(9, 9, 0.5, true)]
        public void IsFarFromWalls_CheckDistanceToWalls(double x, double y, double minDist, bool expected)
        {
            Room room = new Room(_squareRoom);
            Point3D point = new Point3D(x, y, 0);

            bool result = room.IsFarFromWalls(point, minDist);

            Assert.Equal(expected, result);
        }
    }
}
