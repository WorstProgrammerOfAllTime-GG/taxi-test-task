using DeliverySystem.Models;
using DeliverySystem.Status;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection.PortableExecutable;
using System.Text;

namespace DeliverySystem.Services
{
    public class Map
    {
        private object locker = new object();
        private Dictionary<Coordinates, Driver> _driversOnMap = new Dictionary<Coordinates, Driver>();
        public IReadOnlyDictionary<Coordinates, Driver> DriversOnMap
        {
            get
            {
                lock (locker)
                {
                    return new Dictionary<Coordinates, Driver>(_driversOnMap);
                }
            }
        }

        private Random random = new Random();
        public int M { get; }
        public int N { get; }

        public Map(int m, int n)
        {
            M = m; N = n;
            Console.WriteLine($"Создана карта размерностью {M} * {N}");
        }

        public bool TryOccupyDriver(Driver driver)
        {
            lock (locker)
            {  
                    if (driver.Status == StatusDriver.Free)
                    {
                        driver.Status = StatusDriver.Busy;
                        return true;
                    }
                    return false;           
            }
        }

        public bool TryAddDriver(string id, Coordinates coordinates, out Driver ? driver)
        {
            lock (locker)
            {
                driver = null;

                var newDriver = new Driver { ID = id };

                if (AddDriverCoordinates(coordinates.X, coordinates.Y, newDriver))
                {
                    driver = newDriver;
                    return true;
                }

                return false;
            }

        }
        public bool AddDriverCoordinates(int x, int y, Driver driver)
        {
            lock(locker)
            {
                var coordinates = new Coordinates(x, y);

                if (_driversOnMap.TryAdd(coordinates, driver))
                {
                    driver.Coordinates = coordinates;
                    return true;
                }
                return false;
            }         
        }

        public bool TryChangeDriverCoordinates(Driver ? driver, int x, int y)
        {
            lock( locker)
            {
                if (driver == null)
                    throw new ArgumentException(nameof(driver), "Непредвиденная работа программы : driver is null");

                if (!VerificationValidCoordinates(x, y))
                    return false;
                Coordinates newCoords = new Coordinates(x, y);
                if (_driversOnMap.TryAdd(newCoords, driver))
                {
                    RemoveOldCoordinates(driver);
                    driver.Coordinates = newCoords;
                    Console.WriteLine($"Водителю {driver.ID} установлены новые координаты : X:{driver.Coordinates.X} и Y:{driver.Coordinates.Y}");

                    return true;
                }
                return false;
            }
        }

        public void RemoveOldCoordinates(Driver driver)
        {
            lock (locker)
            {
                _driversOnMap.Remove(driver.Coordinates);
            }
        }

        public bool VerificationValidCoordinates(int x, int y)
        {
            return x >= 0 && x < N && y >= 0 && y < M;
        }

        public Driver? DriverSearchByID(string id)
        {
            lock(locker)
            {
                if (string.IsNullOrEmpty(id)) return null;

                return _driversOnMap.Values.FirstOrDefault(d => d.ID == id);
            }
        }
    }
}
