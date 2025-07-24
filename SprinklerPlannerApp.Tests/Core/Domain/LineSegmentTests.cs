using Xunit;
using SprinklerPlannerApp.Core.Domain;

namespace SprinklerPlannerApp.Tests.Core.Domain
{
    public class LineSegmentTests
    {
        [Fact]
        public void Constructor_ValidPoints_CreatesLineSegment()
        {
            // Arrange
            Point3D start = new Point3D(0, 0, 0);
            Point3D end = new Point3D(10, 10, 10);

            // Act
            LineSegment segment = new LineSegment(start, end);

            // Assert
            Assert.Equal(start, segment.Start);
            Assert.Equal(end, segment.End);
        }

        [Fact]
        public void GetClosestPoint_PointOnLine_ReturnsSamePoint()
        {
            // Arrange
            Point3D start = new Point3D(0, 0, 0);
            Point3D end = new Point3D(10, 0, 0);
            LineSegment segment = new LineSegment(start, end);
            Point3D testPoint = new Point3D(5, 0, 0);

            // Act
            Point3D closest = segment.GetClosestPoint(testPoint);

            // Assert
            Assert.Equal(5.0, closest.X, 2);
            Assert.Equal(0.0, closest.Y, 2);
            Assert.Equal(0.0, closest.Z, 2);
        }

        [Fact]
        public void GetClosestPoint_PointOffLine_ReturnsProjection()
        {
            // Arrange
            Point3D start = new Point3D(0, 0, 0);
            Point3D end = new Point3D(10, 0, 0);
            LineSegment segment = new LineSegment(start, end);
            Point3D testPoint = new Point3D(5, 5, 0);

            // Act
            Point3D closest = segment.GetClosestPoint(testPoint);

            // Assert
            Assert.Equal(5.0, closest.X, 2);
            Assert.Equal(0.0, closest.Y, 2);
            Assert.Equal(0.0, closest.Z, 2);
        }

        [Fact]
        public void GetClosestPoint_PointBeyondEnd_ReturnsEndPoint()
        {
            // Arrange
            Point3D start = new Point3D(0, 0, 0);
            Point3D end = new Point3D(10, 0, 0);
            LineSegment segment = new LineSegment(start, end);
            Point3D testPoint = new Point3D(15, 0, 0);

            // Act
            Point3D closest = segment.GetClosestPoint(testPoint);

            // Assert
            Assert.Equal(10.0, closest.X, 2);
            Assert.Equal(0.0, closest.Y, 2);
            Assert.Equal(0.0, closest.Z, 2);
        }

        [Fact]
        public void GetClosestPoint_PointBeforeStart_ReturnsStartPoint()
        {
            // Arrange
            Point3D start = new Point3D(5, 5, 5);
            Point3D end = new Point3D(10, 10, 10);
            LineSegment segment = new LineSegment(start, end);
            Point3D testPoint = new Point3D(0, 0, 0);

            // Act
            Point3D closest = segment.GetClosestPoint(testPoint);

            // Assert
            Assert.Equal(5.0, closest.X, 2);
            Assert.Equal(5.0, closest.Y, 2);
            Assert.Equal(5.0, closest.Z, 2);
        }
    }
}
