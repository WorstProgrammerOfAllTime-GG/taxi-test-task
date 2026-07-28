using maxim_technology_task.Models;
using maxim_technology_task.Status;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace maxim_technology_task.Services
{
    public class Map
    {
        private Dictionary<Coordinates, Driver> _driversOnMap = new Dictionary<Coordinates, Driver>();
        public IReadOnlyDictionary<Coordinates, Driver> DriversOnMap => _driversOnMap;
        private Random random = new Random();
        public int M { get; }
        public int N { get; }

        public Map(int m, int n)
        {
            M = m; N = n;
            Console.WriteLine($"Создана карта размерностью {M} * {N}");
        }

        public bool AddDriverCoordinates(int x, int y, Driver driver)
        {

            if (!VerificationValidCoordinates(x, y))
            {
                return false; 
            }
            var coordinates = new Coordinates(x, y);

            if (_driversOnMap.TryAdd(coordinates, driver))
            {           
                driver.Coordinates = coordinates; 
                return true;
            }
            return false;
        }

        public bool TryChangeDriverCoordinates(Driver driver, int x, int y)
        {
            if (driver == null) 
                throw new ArgumentException(nameof(driver), "Непредвиденная работа программы : driver is null");

            if (!VerificationValidCoordinates(x, y))
                return false;
            Coordinates newCoords = new Coordinates(x, y);
            if (_driversOnMap.TryAdd(newCoords, driver)) 
            {
                RemoveOldCoordiantes(driver);
                driver.Coordinates = newCoords;
                Console.WriteLine($"Водителю {driver.ID} установлены новые координаты : X:{driver.Coordinates.X} и Y:{driver.Coordinates.Y}");
                
                return true;
            }
            return false;
        }

        private void RemoveOldCoordiantes(Driver driver)
        {
            _driversOnMap.Remove(driver.Coordinates);
        }

        public bool VerificationValidCoordinates(int x, int y)
        {
            return x >= 0 && x < M && y >= 0 && y < N;
        }
    }
}
