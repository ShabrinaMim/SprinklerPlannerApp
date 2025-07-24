using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace SprinklerPlannerApp.Infrastructure.Services
{
    public class SprinklerPlannerService : ISprinklerPlannerService
    {
        private readonly Room _room;
        private readonly List<LineSegment> _pipes;
        private readonly List<Point3D> _placedSprinklers;
        private readonly ILogger<SprinklerPlannerService> _logger;

        private const double _minSpacing = 2500;

        public SprinklerPlannerService(Room room, List<LineSegment> pipes, ILogger<SprinklerPlannerService> logger)
        {
            _room = room;
            _pipes = pipes;
            _logger = logger;
            _placedSprinklers = new List<Point3D>();

            _logger.LogInformation("SprinklerPlannerService initialized with {Count} pipes", pipes.Count);
        }

        public List<(Point3D Sprinkler, Point3D ClosestPipePoint)> PlanSprinklers()
        {
            _logger.LogInformation("Starting sprinkler planning...");
            List<(Point3D, Point3D)> sprinklerResults = new();

            (double minX, double maxX, double minY, double maxY) = _room.GetBounds();
            double zCoordinate = _room.Corners.First().Z;

            for (double x = minX; x <= maxX; x += _minSpacing)
            {
                for (double y = minY; y <= maxY; y += _minSpacing)
                {
                    Point3D candidate = new(x, y, zCoordinate);

                    if (!IsPlacementValid(candidate))
                    {
                        _logger.LogDebug("Invalid placement at {X}, {Y}", x, y);
                        continue;
                    }

                    Point3D? closestPipePoint = FindNearestPipePoint(candidate);
                    if (closestPipePoint == null)
                    {
                        _logger.LogWarning("No valid pipe connection found for sprinkler at {X}, {Y}", x, y);
                        continue;
                    }

                    _placedSprinklers.Add(candidate);
                    sprinklerResults.Add((candidate, closestPipePoint));
                    _logger.LogDebug("Sprinkler placed at {X}, {Y}", x, y);
                }
            }

            _logger.LogInformation("Planning complete. Total sprinklers placed: {Count}", sprinklerResults.Count);
            return sprinklerResults;
        }

        private bool IsPlacementValid(Point3D sprinklerLocation)
        {
            return _room.IsInside(sprinklerLocation)
                && _room.IsFarFromWalls(sprinklerLocation, _minSpacing)
                && _placedSprinklers.All(existing => existing.DistanceTo(sprinklerLocation) >= _minSpacing);
        }

        private Point3D? FindNearestPipePoint(Point3D sprinklerLocation)
        {
            Point3D? nearestPoint = null;
            double minDistance = double.MaxValue;

            foreach (var pipe in _pipes)
            {
                Point3D projected = pipe.GetClosestPoint(sprinklerLocation);
                double distance = projected.DistanceTo(sprinklerLocation);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPoint = projected;
                }
            }

            return nearestPoint;
        }
    }
}
