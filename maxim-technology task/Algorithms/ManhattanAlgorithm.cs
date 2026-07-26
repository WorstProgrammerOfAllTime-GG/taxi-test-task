using maxim_technology_task.Models;
using maxim_technology_task.Services;
using maxim_technology_task.Status;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace maxim_technology_task.Algorithms
{
    public class ManhattanAlgorithm : IAlgorithm
    {
        private const string TITLE = "Манхэттенский алгоритм";
        private readonly Map _map;
        
        public ManhattanAlgorithm(Map map)
        {
            _map = map;
        }
        public List<Driver> SearchDrivers(Coordinates coordClient)
        {
            var driversOnMap = _map.DriversOnMap;
            var result = new List<(int, Driver)>();

            Console.WriteLine($"Поиск водителей начался, алгоритм поиска: [{TITLE}]...");
            foreach (var dataDriver in driversOnMap)
            {     
                if (dataDriver.Value.Status != StatusDriver.Free)
                    continue;
                int distance = Math.Abs(dataDriver.Key.X - coordClient.X) + Math.Abs(dataDriver.Key.Y - coordClient.Y);
                result.Add((distance, dataDriver.Value));
            }      
            var drivers = result.OrderBy(x=> x.Item1).Take(5).Select(x=> x.Item2).ToList();
            return drivers; 
        }
    }
}
