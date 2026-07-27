using DeliverySystem;
using DeliverySystem.Models;
using DeliverySystem.Services;
using DeliverySystem.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Algorithms
{
    public class SectorAlgorithm : IAlgorithm
    {
        private readonly Map _map;
        public SectorAlgorithm(Map map)
        {
            _map = map;
        }
        public List<Driver> SearchDrivers(Coordinates coordClient)
        {
            List<(string, int, Driver)> _cardinalPointsDrivers = new List<(string, int, Driver)>();
            var drivers = _map.DriversOnMap;
            foreach (var dataDriver in drivers)
            {
                if (dataDriver.Value.Status == StatusDriver.Busy)
                    continue;
                int x = dataDriver.Value.Coordinates.X - coordClient.X;
                int y = dataDriver.Value.Coordinates.Y - coordClient.Y;
                int distance = Math.Abs(x) + Math.Abs(y);

                if (x>=0 && y>=0)
                {
                    _cardinalPointsDrivers.Add(("NorthEast", distance, dataDriver.Value));
                } else if (x<0 && y>=0)
                {
                    _cardinalPointsDrivers.Add(("NorthWest", distance, dataDriver.Value));
                } else if(x<0 && y<0)
                {
                    _cardinalPointsDrivers.Add(("SouthWest", distance, dataDriver.Value));
                } else if(x>=0 && y<0)
                {
                    _cardinalPointsDrivers.Add(("SouthEast", distance, dataDriver.Value));
                }
            }
            var selectedDrivers = new List<(Driver, int)>();
            var sectors = _cardinalPointsDrivers.GroupBy(x=> x.Item1).Select(group=> group.OrderBy(g=> g.Item2).ToList()).ToList();

            while (selectedDrivers.Count < 5 && sectors.Any(s => s.Count > 0))
            {
                foreach (var sector in sectors)
                {
                    if (selectedDrivers.Count == 5)
                        break;            
                    if (sector.Count > 0)
                    {
                        selectedDrivers.Add((sector[0].Item3, sector[0].Item2));
                        sector.RemoveAt(0);
                    }
                }
            }

            return selectedDrivers.OrderBy(x=> x.Item2).Select(x=> x.Item1).Take(5).ToList();

        }     
        
    }
}
