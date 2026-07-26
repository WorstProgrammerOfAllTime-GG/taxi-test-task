using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task
{
    public class GridSearchAlgorithm : IAlgorithm
    {
        private const string TITLE = "Расширяющийся радиус";

        private readonly Map _map;
        
        public GridSearchAlgorithm(Map map)
        {
            _map = map;
        }

        public List<Driver> SearchDrivers(Coordinates coordsCleint)
        {
            const int maxRadius = 100;
            var drivers = _map.DriversOnMap;
            int currentDistance = 0;       
            List<Driver> result = new List<Driver>();
            Console.WriteLine($"Поиск водителей начался, алгоритм поиска: [{TITLE}]...");
            while (result.Count < 5 && currentDistance<=maxRadius)
            {
                foreach (var coords in GetCoordinatesByRadius(coordsCleint, currentDistance))
                {
                    if (drivers.TryGetValue(coords, out Driver driver))
                    {
                        result.Add(driver);
                        driver.Status = StatusDriver.Busy;
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
