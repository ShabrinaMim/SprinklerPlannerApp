using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SprinklerPlannerApp.Core.Domain;
using SprinklerPlannerApp.Core.Interfaces;

namespace SprinklerPlannerApp.Infrastructure.Data
{
    public class RoomDataSeeder : IRoomDataSeeder
    {
        public Room LoadFromCsv(string filePath)
        {
            List<Point3D> corners = new List<Point3D>();
            foreach (string line in File.ReadAllLines(filePath))
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    try
                    {
                        double x = double.Parse(parts[0], CultureInfo.InvariantCulture);
                        double y = double.Parse(parts[1], CultureInfo.InvariantCulture);
                        double z = double.Parse(parts[2], CultureInfo.InvariantCulture);
                        corners.Add(new Point3D(x, y, z));
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                }
            }
            return new Room(corners);
        }

    }
}
