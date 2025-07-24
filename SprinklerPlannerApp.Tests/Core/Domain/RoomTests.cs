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
            // Arrange
            Room room = new Room(_squareRoom);

            // Act
            (double minX, double maxX, double minY, double maxY) = room.GetBounds();

            // Assert
            Assert.Equal(0, minX);
            Assert.Equal(10, maxX);
            Assert.Equal(0, minY);
            Assert.Equal(10, maxY);
        }

        [Theory]
        [InlineData(5, 5, true)]     // clearly inside
        [InlineData(0, 0, true)]     // corner — considered inside
        [InlineData(10, 0, false)]   // edge — treated as outside by your algo
        [InlineData(-1, -1, false)]  // outside
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
        [InlineData(5, 5, 2, true)]   // Inside and far enough from walls
        [InlineData(0.5, 0.5, 2, false)] // Too close to wall
        [InlineData(9, 9, 0.5, true)]  // Inside and slightly away
        public void IsFarFromWalls_CheckDistanceToWalls(double x, double y, double minDist, bool expected)
        {
            // Arrange
            Room room = new Room(_squareRoom);
            Point3D point = new Point3D(x, y, 0);

            // Act
            bool result = room.IsFarFromWalls(point, minDist);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
