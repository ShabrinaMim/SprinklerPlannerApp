using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Core.Interfaces;

namespace SprinklerPlannerApp.Infrastructure.Data
{
    public class PipeDataSeeder : IPipeDataSeeder
    {
        public List<LineSegment> LoadFromCsv(string filePath)
        {
            List<LineSegment> pipes = new List<LineSegment>();

            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(',');
                if (parts.Length == 6)
                {
                    try
                    {
                        Point3D start = new Point3D(
                            double.Parse(parts[0], CultureInfo.InvariantCulture),
                            double.Parse(parts[1], CultureInfo.InvariantCulture),
                            double.Parse(parts[2], CultureInfo.InvariantCulture)
                        );

                        Point3D end = new Point3D(
                            double.Parse(parts[3], CultureInfo.InvariantCulture),
                            double.Parse(parts[4], CultureInfo.InvariantCulture),
                            double.Parse(parts[5], CultureInfo.InvariantCulture)
                        );

                        pipes.Add(new LineSegment(start, end));
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }
            }

            return pipes;
        }
    }
}
