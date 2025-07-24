using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Core.Interfaces;

namespace SprinklerPlannerApp.Infrastructure.Output
{
    public class ConsoleOutputPrinter : IOutputPrinter
    {
        private readonly ILogger<ConsoleOutputPrinter> _logger;

        public ConsoleOutputPrinter(ILogger<ConsoleOutputPrinter> logger)
        {
            _logger = logger;
        }

        public void PrintSprinklerResults(List<(Point3D Sprinkler, Point3D ClosestPipePoint)> sprinklerResults)
        {
            _logger.LogInformation("Total Sprinklers Placed: {Count}", sprinklerResults.Count);

            foreach ((Point3D sprinkler, Point3D pipePoint) in sprinklerResults)
            {
                _logger.LogInformation("Sprinkler: {Sprinkler} → Pipe: {Pipe}", FormatPoint(sprinkler), FormatPoint(pipePoint));
            }
        }

        public void ExportToCsv(List<(Point3D Sprinkler, Point3D ClosestPipePoint)> sprinklerResults, string relativePath)
        {
            string fullPath = Path.GetFullPath(Path.Combine("SprinklerPlannerApp", "Resources", "output.csv"));

            string? directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            using StreamWriter writer = new StreamWriter(fullPath);
            writer.WriteLine("SprinklerX,SprinklerY,SprinklerZ,PipeX,PipeY,PipeZ");

            foreach (var result in sprinklerResults)
            {
                writer.WriteLine($"{result.Sprinkler.X},{result.Sprinkler.Y},{result.Sprinkler.Z}," +
                                 $"{result.ClosestPipePoint.X},{result.ClosestPipePoint.Y},{result.ClosestPipePoint.Z}");
            }

            _logger.LogInformation("CSV report saved.");
        }

        private string FormatPoint(Point3D point)
        {
            return $"({point.X:0.00}, {point.Y:0.00}, {point.Z:0.00})";
        }
    }
}
