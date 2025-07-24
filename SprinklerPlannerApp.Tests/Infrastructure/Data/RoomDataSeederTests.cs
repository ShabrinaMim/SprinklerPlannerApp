using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Infrastructure.Data;

namespace SprinklerPlannerApp.Tests.Infrastructure.Data
{
    public class RoomDataSeederTests
    {
        [Fact]
        public void LoadFromCsv_ValidCsv_ReturnsRoomWithPoints()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new[]
            {
                "0,0,0",
                "10,0,0",
                "10,10,0",
                "0,10,0"
            });

            RoomDataSeeder seeder = new RoomDataSeeder();

            // Act
            Room room = seeder.LoadFromCsv(tempFilePath);

            // Assert
            Assert.NotNull(room);
            Assert.Equal(4, room.Corners.Count);

            Assert.Equal(0, room.Corners[0].X, 3);
            Assert.Equal(10, room.Corners[1].X, 3);
            Assert.Equal(10, room.Corners[2].Y, 3);
            Assert.Equal(10, room.Corners[3].Y, 3);

            // Cleanup
            File.Delete(tempFilePath);
        }

        [Fact]
        public void LoadFromCsv_InvalidLines_IgnoresMalformedLines()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new[]
            {
                "0,0,0",
                "invalid,data,ignored",
                "10,10,0"
            });

            RoomDataSeeder seeder = new RoomDataSeeder();

            // Act
            Room room = seeder.LoadFromCsv(tempFilePath);

            // Assert
            Assert.NotNull(room);
            Assert.Equal(2, room.Corners.Count);
            Assert.Equal(0, room.Corners[0].X, 3);
            Assert.Equal(10, room.Corners[1].X, 3);

            // Cleanup
            File.Delete(tempFilePath);
        }
    }
}
