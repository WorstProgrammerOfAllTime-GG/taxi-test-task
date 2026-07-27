using DeliverySystem.Models;
using DeliverySystem.Services;

namespace maxim_technology_task2
{
    public class DriverFactory
    {
        private readonly Map _map;

        public DriverFactory(Map map)
        {
            _map = map;
        }

        public void CreateDrivers()
        {
            var drivers = _map.DriversOnMap;
            int attempts = 0;
            const int maxAttempts = 1000;
            while (drivers.Count < 15 && attempts<maxAttempts)
            {
                attempts++;
                int x = Random.Shared.Next(0, _map.M);
                int y = Random.Shared.Next(0, _map.N);

                if (!_map.VerificationValidCoordinates(x, y))
                {
                    continue;
                }
                Driver driver = new Driver();
                _map.AddDriverCoordinates(x, y, driver);
                Console.WriteLine($"Водитель с ID {driver.ID}");
            }

            if (drivers.Count < 15) throw new Exception("Не удалось разместить всех водителей на карте"); 

        }
    }
}
