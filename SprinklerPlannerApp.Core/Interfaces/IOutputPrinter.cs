using SprinklerPlannerApp.Core.Domain;
using System.Collections.Generic;

namespace SprinklerPlannerApp.Core.Interfaces
{
    public interface IOutputPrinter
    {
        void PrintSprinklerResults(List<(Point3D Sprinkler, Point3D ClosestPipePoint)> sprinklerResults);
        void ExportToCsv(List<(Point3D Sprinkler, Point3D ClosestPipePoint)> sprinklerResults, string filePath);
    }
}
