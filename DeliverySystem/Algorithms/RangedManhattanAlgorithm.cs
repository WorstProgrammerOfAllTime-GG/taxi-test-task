using DeliverySystem.Models;
using DeliverySystem.Services;
using DeliverySystem.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Algorithms
{
    public class RangedManhattanAlgorithm : IAlgorithm
    {
        private const int MAX_ALLOWED_DISTANCE = 50;
        private readonly Map _map;
        public RangedManhattanAlgorithm(Map map)
        {
            _map = map;
        }

        public List<Driver> SearchDrivers(Coordinates coordClient)
        {
            var driversOnMap = _map.DriversOnMap;
            var result = new List<(int,Driver)>();
            foreach (var dataDriver in driversOnMap)
            {
                int distance = Math.Abs(dataDriver.Value.Coordinates.X - coordClient.X) +
                    Math.Abs(dataDriver.Value.Coordinates.Y - coordClient.Y);
                if (dataDriver.Value.Status == StatusDriver.Busy)
                {
                    continue;
                }
                result.Add((distance,dataDriver.Value));
            }

            var driversInRadius = result.Where(x => x.Item1 <= MAX_ALLOWED_DISTANCE)
                .OrderBy(x => x.Item1).Take(5).Select(x=>x.Item2).ToList();

            return driversInRadius;
        }
    }
}
