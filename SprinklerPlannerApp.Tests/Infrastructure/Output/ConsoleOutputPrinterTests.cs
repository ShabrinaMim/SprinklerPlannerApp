using Xunit;
using Moq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Infrastructure.Output;

namespace SprinklerPlannerApp.Tests.Infrastructure.Output
{
    public class ConsoleOutputPrinterTests
    {
        [Fact]
        public void PrintSprinklerResults_LogsEachResult()
        {
            Mock<ILogger<ConsoleOutputPrinter>> mockLogger = new Mock<ILogger<ConsoleOutputPrinter>>();
            ConsoleOutputPrinter printer = new ConsoleOutputPrinter(mockLogger.Object);

            List<(Point3D Sprinkler, Point3D ClosestPipePoint)> data = new List<(Point3D, Point3D)>
            {
                (new Point3D(1, 2, 3), new Point3D(4, 5, 6)),
                (new Point3D(7, 8, 9), new Point3D(10, 11, 12))
            };

            printer.PrintSprinklerResults(data);

            mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((state, _) => state!.ToString()!.Contains("Total Sprinklers Placed")),
                    It.IsAny<Exception?>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);

            mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((state, _) => state!.ToString()!.Contains("Sprinkler:")),
                    It.IsAny<Exception?>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Exactly(2));
        }
    }
}
