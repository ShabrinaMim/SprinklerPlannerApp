using System.Collections.Generic;
using SprinklerPlannerApp.Core.Domain;

namespace SprinklerPlannerApp.Core.Interfaces
{
    public interface IPipeDataSeeder
    {
        List<LineSegment> LoadFromCsv(string filePath);
    }
}
