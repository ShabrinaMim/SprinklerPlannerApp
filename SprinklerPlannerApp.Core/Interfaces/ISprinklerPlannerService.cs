using System.Collections.Generic;
using SprinklerPlannerApp.Core.Domain;

namespace SprinklerPlannerApp.Core.Interfaces
{
    public interface ISprinklerPlannerService
    {
        List<(Point3D Sprinkler, Point3D ClosestPipePoint)> PlanSprinklers();
    }
}
