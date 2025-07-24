using Xunit;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Infrastructure.Data;
using System.Collections.Generic;
using System.IO;

namespace SprinklerPlannerApp.Tests.Infrastructure.Data
{
    public class PipeDataSeederTests
    {
        [Fact]
        public void LoadFromCsv_ValidCsv_ReturnsCorrectLineSegments()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new[]
            {
                "0,0,0,10,0,0",
                "5,5,5,15,5,5"
            });

            PipeDataSeeder seeder = new PipeDataSeeder();

            // Act
            List<LineSegment> pipes = seeder.LoadFromCsv(tempFilePath);

            // Assert
            Assert.Equal(2, pipes.Count);

            Assert.Equal(0, pipes[0].Start.X, 3);
            Assert.Equal(10, pipes[0].End.X, 3);

            Assert.Equal(5, pipes[1].Start.X, 3);
            Assert.Equal(15, pipes[1].End.X, 3);

            File.Delete(tempFilePath); // Cleanup
        }

        [Fact]
        public void LoadFromCsv_InvalidLine_IgnoresMalformedLines()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new[]
            {
                "0,0,0,10,0,0",
                "invalid,line,that,should,be,ignored",
                "1,1,1,2,2,2"
            });

            PipeDataSeeder seeder = new PipeDataSeeder();

            // Act
            List<LineSegment> pipes = seeder.LoadFromCsv(tempFilePath);

            // Assert
            Assert.Equal(2, pipes.Count);
            Assert.Equal(0, pipes[0].Start.X, 3);
            Assert.Equal(1, pipes[1].Start.X, 3);

            File.Delete(tempFilePath); // Cleanup
        }
    }
}
