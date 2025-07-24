using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Infrastructure.Services;
using Xunit;

namespace SprinklerPlannerApp.Tests.Infrastructure.Services
{
    public class SprinklerPlannerServiceTests
    {
        [Fact]
        public void PlanSprinklers_WithValidRoomAndPipes_PlacesSprinklersCorrectly()
        {
            // Arrange
            List<Point3D> corners = new List<Point3D>
            {
                new Point3D(0, 0, 0),
                new Point3D(10000, 0, 0),
                new Point3D(10000, 10000, 0),
                new Point3D(0, 10000, 0)
            };
            Room room = new Room(corners);

            List<LineSegment> pipes = new List<LineSegment>
            {
                new LineSegment(
                    new Point3D(2500, 2500, 0),
                    new Point3D(7500, 2500, 0))
            };

            Mock<ILogger<SprinklerPlannerService>> loggerMock = new Mock<ILogger<SprinklerPlannerService>>();
            SprinklerPlannerService planner = new SprinklerPlannerService(room, pipes, loggerMock.Object);

            // Act
            List<(Point3D Sprinkler, Point3D ClosestPipePoint)> results = planner.PlanSprinklers();

            // Assert
            Assert.NotEmpty(results);
            foreach ((Point3D sprinkler, Point3D pipePoint) in results)
            {
                Assert.True(room.IsInside(sprinkler));
                Assert.True(room.IsFarFromWalls(sprinkler, 2500));
                Assert.NotNull(pipePoint);
            }
        }

        [Fact]
        public void PlanSprinklers_WhenNoValidPipes_ReturnsEmptyResult()
        {
            // Arrange
            List<Point3D> corners = new List<Point3D>
            {
                new Point3D(0, 0, 0),
                new Point3D(10000, 0, 0),
                new Point3D(10000, 10000, 0),
                new Point3D(0, 10000, 0)
            };
            Room room = new Room(corners);
            List<LineSegment> emptyPipes = new List<LineSegment>();

            Mock<ILogger<SprinklerPlannerService>> loggerMock = new Mock<ILogger<SprinklerPlannerService>>();
            SprinklerPlannerService planner = new SprinklerPlannerService(room, emptyPipes, loggerMock.Object);

            // Act
            List<(Point3D Sprinkler, Point3D ClosestPipePoint)> results = planner.PlanSprinklers();

            // Assert
            Assert.Empty(results);
        }

        [Fact]
        public void PlanSprinklers_WithTooSmallRoom_ReturnsEmpty()
        {
            // Arrange — Room too small for 2500mm spacing
            List<Point3D> corners = new List<Point3D>
            {
                new Point3D(0, 0, 0),
                new Point3D(2000, 0, 0),
                new Point3D(2000, 2000, 0),
                new Point3D(0, 2000, 0)
            };
            Room smallRoom = new Room(corners);

            List<LineSegment> pipes = new List<LineSegment>
            {
                new LineSegment(
                    new Point3D(1000, 1000, 0),
                    new Point3D(1500, 1500, 0))
            };

            Mock<ILogger<SprinklerPlannerService>> loggerMock = new Mock<ILogger<SprinklerPlannerService>>();
            SprinklerPlannerService planner = new SprinklerPlannerService(smallRoom, pipes, loggerMock.Object);

            // Act
            List<(Point3D Sprinkler, Point3D ClosestPipePoint)> results = planner.PlanSprinklers();

            // Assert
            Assert.Empty(results);
        }
    }
}
