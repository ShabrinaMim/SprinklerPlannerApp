using System;

namespace SprinklerPlannerApp.Core.Domain
{
    public class Point3D
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double DistanceTo(Point3D otherPoint)
        {
            double dx = X - otherPoint.X;
            double dy = Y - otherPoint.Y;
            double dz = Z - otherPoint.Z;

            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public override string ToString()
        {
            return string.Format("({0:0.00}, {1:0.00}, {2:0.00})", X, Y, Z);
        }
    }
}
