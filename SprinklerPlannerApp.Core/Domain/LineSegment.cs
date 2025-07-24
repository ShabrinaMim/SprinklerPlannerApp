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
            double deltaX = End.X - Start.X;
            double deltaY = End.Y - Start.Y;
            double deltaZ = End.Z - Start.Z;

            double vectorToPointX = point.X - Start.X;
            double vectorToPointY = point.Y - Start.Y;
            double vectorToPointZ = point.Z - Start.Z;

            double segmentLengthSquared = deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ;
            if (segmentLengthSquared == 0.0)
            {
                return Start;
            }

            double projectionFactor = (vectorToPointX * deltaX + vectorToPointY * deltaY + vectorToPointZ * deltaZ) / segmentLengthSquared;
            projectionFactor = Math.Max(0.0, Math.Min(1.0, projectionFactor));

            double closestX = Start.X + projectionFactor * deltaX;
            double closestY = Start.Y + projectionFactor * deltaY;
            double closestZ = Start.Z + projectionFactor * deltaZ;

            return new Point3D(closestX, closestY, closestZ);
        }
    }
}
