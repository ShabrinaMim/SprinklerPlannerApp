using SprinklerPlannerApp.Core.Domain;
using System.Collections.Generic;

namespace SprinklerPlannerApp.Core.Interfaces
{
    public interface IRoomDataSeeder
    {
        Room LoadFromCsv(string filePath);
    }
}
