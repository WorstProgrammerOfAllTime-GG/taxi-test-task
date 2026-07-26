using maxim_technology_task.Models;
using maxim_technology_task.Services;
using maxim_technology_task.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task.Algorithms
{
    public class RangedManhattanAlgorithm : IAlgorithm
    {
        private const string TITLE = "Манхэттен с ограничением радиуса";
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
            Console.WriteLine($"Поиск водителей начался, алгоритм поиска: [{TITLE}]...");
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
