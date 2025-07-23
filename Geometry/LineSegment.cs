namespace SprinklerPlannerApp.Geometry
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

            double px = point.X - Start.X;
            double py = point.Y - Start.Y;
            double pz = point.Z - Start.Z;

            double abLenSquared = dx * dx + dy * dy + dz * dz;
            if (abLenSquared == 0) return Start;

            double t = (px * dx + py * dy + pz * dz) / abLenSquared;
            t = Math.Max(0, Math.Min(1, t)); // Clamp between 0 and 1

            return new Point3D(
                Start.X + t * dx,
                Start.Y + t * dy,
                Start.Z + t * dz
            );
        }
    }
}
