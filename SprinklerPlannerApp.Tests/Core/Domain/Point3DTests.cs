using Xunit;
using SprinklerPlannerApp.Core.Domain;

namespace SprinklerPlannerApp.Tests.Core.Domain
{
    public class Point3DTests
    {
        [Fact]
        public void Constructor_ValidCoordinates_CreatesPoint()
        {
            // Arrange & Act
            Point3D point = new Point3D(100.5, 200.7, 300.9);

            // Assert
            Assert.Equal(100.5, point.X);
            Assert.Equal(200.7, point.Y);
            Assert.Equal(300.9, point.Z);
        }

        [Fact]
        public void DistanceTo_SamePoint_ReturnsZero()
        {
            // Arrange
            Point3D point1 = new Point3D(100, 200, 300);
            Point3D point2 = new Point3D(100, 200, 300);

            // Act
            double distance = point1.DistanceTo(point2);

            // Assert
            Assert.Equal(0, distance, 10);
        }

        [Fact]
        public void DistanceTo_3D_Pythagorean_ReturnsCorrectDistance()
        {
            // Arrange
            Point3D point1 = new Point3D(0, 0, 0);
            Point3D point2 = new Point3D(3, 4, 12);

            // Act
            double distance = point1.DistanceTo(point2);

            // Assert
            Assert.Equal(13.0, distance, 2);
        }

        [Theory]
        [InlineData(0, 0, 0, 10, 0, 0, 10.0)]
        [InlineData(1, 1, 1, 4, 5, 13, 13.0)]
        public void DistanceTo_VariousPoints_ReturnsCorrectDistance(
            double x1, double y1, double z1,
            double x2, double y2, double z2,
            double expectedDistance)
        {
            // Arrange
            Point3D point1 = new Point3D(x1, y1, z1);
            Point3D point2 = new Point3D(x2, y2, z2);

            // Act
            double distance = point1.DistanceTo(point2);

            // Assert
            Assert.Equal(expectedDistance, distance, 1);
        }
    }
}
