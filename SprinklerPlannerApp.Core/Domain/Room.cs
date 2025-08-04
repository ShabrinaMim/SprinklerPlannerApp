using System;
using System.Collections.Generic;

namespace SprinklerPlannerApp.Core.Domain
{
    public class Room
    {
        public List<Point3D> Corners { get; }

        public Room(List<Point3D> corners)
        {
            Corners = corners;
        }

        public (double minX, double maxX, double minY, double maxY) GetBounds()
        {
            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            for (int i = 0; i < Corners.Count; i++)
            {
                double x = Corners[i].X;
                double y = Corners[i].Y;

                if (x < minX) minX = x;
                if (x > maxX) maxX = x;
                if (y < minY) minY = y;
                if (y > maxY) maxY = y;
            }

            return (minX, maxX, minY, maxY);
        }

        public bool IsInside(Point3D point)
        {
            int cornerCount = Corners.Count;
            bool inside = false;

            for (int i = 0, j = cornerCount - 1; i < cornerCount; j = i++)
            {
                double xi = Corners[i].X;
                double yi = Corners[i].Y;
                double xj = Corners[j].X;
                double yj = Corners[j].Y;

                if (Math.Abs(yj - yi) < 1e-10)
                {
                    continue;
                }

                bool intersects = (yi > point.Y) != (yj > point.Y) &&
                    point.X < (xj - xi) * (point.Y - yi) / (yj - yi) + xi;

                if (intersects)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        public bool IsFarFromWalls(Point3D point, double minDistance)
        {
            int cornerCount = Corners.Count;

            for (int i = 0; i < cornerCount; i++)
            {
                Point3D start = Corners[i];
                Point3D end = Corners[(i + 1) % cornerCount];

                LineSegment segment = new LineSegment(start, end);
                double distance = segment.GetDistanceTo(point);

                if (distance < minDistance)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
