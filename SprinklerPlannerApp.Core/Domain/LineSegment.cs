using System;

namespace SprinklerPlannerApp.Core.Domain
{
    public class LineSegment
    {
        public Point3D Start { get; }
        public Point3D End { get; }

        public LineSegment(Point3D start, Point3D end)
        {
            Start = start;
            End = end;
        }

        public Point3D GetClosestPoint(Point3D point)
        {
            double dx = End.X - Start.X;
            double dy = End.Y - Start.Y;
            double dz = End.Z - Start.Z;

            double segmentLengthSquared = dx * dx + dy * dy + dz * dz;

            if (segmentLengthSquared == 0.0)
            {
                return Start;
            }

            double projectionFactor = ((point.X - Start.X) * dx + (point.Y - Start.Y) * dy + (point.Z - Start.Z) * dz) / segmentLengthSquared;

            projectionFactor = Math.Max(0.0, Math.Min(1.0, projectionFactor));

            double closestX = Start.X + projectionFactor * dx;
            double closestY = Start.Y + projectionFactor * dy;
            double closestZ = Start.Z + projectionFactor * dz;

            return new Point3D(closestX, closestY, closestZ);
        }

        public double GetDistanceTo(Point3D point)
        {
            Point3D closest = GetClosestPoint(point);
            return point.DistanceTo(closest);
        }
    }
}
