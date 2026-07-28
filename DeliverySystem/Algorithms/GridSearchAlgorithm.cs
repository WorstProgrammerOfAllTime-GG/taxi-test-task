using DeliverySystem.Models;
using DeliverySystem.Services;
using DeliverySystem.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Algorithms
{
    public class GridSearchAlgorithm : IAlgorithm
    {
        private readonly Map _map;

        public GridSearchAlgorithm(Map map)
        {
            _map = map;
        }

        public List<Driver> SearchDrivers(Coordinates coordsClient)
        {
            const int maxRadius = 100;
            int currentDistance = 0;
            List<Driver> result = new List<Driver>();

            while (result.Count < 5 && currentDistance <= maxRadius)
            {
                foreach (var coords in GetCoordinatesByRadius(coordsClient, currentDistance))
                {
                    var driverOnPoint = _map.DriversOnMap.Values
                        .FirstOrDefault(d => d.Coordinates.X == coords.X && d.Coordinates.Y == coords.Y);

                    if (driverOnPoint != null)
                    {
                        if (driverOnPoint.Status == StatusDriver.Busy)
                        {
                            continue;
                        }

                        result.Add(driverOnPoint);

                        if (result.Count == 5)
                        {
                            return result;
                        }
                    }
                }
                currentDistance++;
            }

            return result;
        }

        public IEnumerable<Coordinates> GetCoordinatesByRadius(Coordinates center, int currentDistance)
        {
            for (int x = center.X - currentDistance; x <= center.X + currentDistance; x++)
            {
                for (int y = center.Y - currentDistance; y <= center.Y + currentDistance; y++)
                {
                    if (!_map.VerificationValidCoordinates(x, y))
                        continue;
                    int distance = Math.Abs(x - center.X) + Math.Abs(y - center.Y);
                    if (distance == currentDistance)
                    {
                        yield return new Coordinates(x, y);
                    }
                }
            }
        }
    }
}
