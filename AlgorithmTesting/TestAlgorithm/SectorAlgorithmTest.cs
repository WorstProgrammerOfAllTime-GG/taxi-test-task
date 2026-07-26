using maxim_technology_task.Algorithms;
using maxim_technology_task.Models;
using maxim_technology_task.Services;
using maxim_technology_task.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing.TestAlgorithm
{
    [TestFixture]
    public class SectorAlgorithmTest
    {
        [Test]
        public void SearchDrivers_SectorAlgorithm()
        {
            Map map = new Map(100, 100);
            Coordinates coordsClient = new Coordinates(50, 50);
  
            var northEastDriver = new Driver();
            map.AddDriverCoordinates(60, 60, northEastDriver);
    
            var northWestDriver = new Driver();
            map.AddDriverCoordinates(45, 55, northWestDriver);

            var southWestDriver = new Driver();
            map.AddDriverCoordinates(30, 30, southWestDriver);
      
            var southEastDriver = new Driver();
            map.AddDriverCoordinates(65, 35, southEastDriver);
      
            var fifthDriver = new Driver();
            map.AddDriverCoordinates(25, 75, fifthDriver);

            var algorithm = new SectorAlgorithm(map);
           
            List<Driver> drivers = algorithm.SearchDrivers(coordsClient);

            Assert.That(drivers.Count, Is.EqualTo(5));
            Assert.That(drivers[0], Is.EqualTo(northWestDriver));
        }
        [Test]
        public void SearchDrivers_IgnoresBusyDrivers()
        {
            var map = new Map(100, 100);
            var coordsClient = new Coordinates(50, 50);

            var busyDriver = new Driver { Status = StatusDriver.Busy };
            var freeDriver = new Driver();

            map.AddDriverCoordinates(51, 51, busyDriver);
            map.AddDriverCoordinates(60, 60, freeDriver);

            var algorithm = new SectorAlgorithm(map);

            List<Driver> drivers = algorithm.SearchDrivers(coordsClient);

            Assert.That(drivers.Count, Is.EqualTo(1));
            Assert.That(drivers[0], Is.EqualTo(freeDriver));
        }

        [Test]
        public void SearchDrivers_LimitsResultToFiveDrivers()
        {
            var map = new Map(100, 100);
            var coordsClient = new Coordinates(50, 50);

            for (int i = 1; i <= 7; i++)
            {
                map.AddDriverCoordinates(50 + i, 50, new Driver());
            }

            var algorithm = new SectorAlgorithm(map);

            List<Driver> drivers = algorithm.SearchDrivers(coordsClient);

            Assert.That(drivers.Count, Is.EqualTo(5));
        }

        [Test]
        public void SearchDrivers_NoFreeDriversOnMap()
        {
            var map = new Map(100, 100);
            var coordsClient = new Coordinates(50, 50);
            var algorithm = new SectorAlgorithm(map);

            List<Driver> drivers = algorithm.SearchDrivers(coordsClient);

            Assert.That(drivers, Is.Empty);
        }
    }
}
