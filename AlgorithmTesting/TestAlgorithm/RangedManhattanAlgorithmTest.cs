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
    public class RangedManhattanAlgorithmTest
    {
        [Test]
        public void SearchDrivers_RangedManhattanAlgorithm()
        {
            Map map = new Map(100,100);

            var coordsClient = new Coordinates(50,50);

            var closestDistanceDriver = new Driver();
            var farDistanceDriver = new Driver();

            map.AddDriverCoordinates(45,36, closestDistanceDriver);
            map.AddDriverCoordinates(84,92, farDistanceDriver);

            var algorithm = new RangedManhattanAlgorithm(map);

            List<Driver> drivers = algorithm.SearchDrivers(coordsClient);
            Assert.That(drivers.Count, Is.EqualTo(1));
            Assert.That(drivers[0], Is.EqualTo(closestDistanceDriver));
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

            var algorithm = new RangedManhattanAlgorithm(map);

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

            var algorithm = new RangedManhattanAlgorithm(map);

            List<Driver> drivers = algorithm.SearchDrivers(coordsClient);

            Assert.That(drivers.Count, Is.EqualTo(5));
        }

        [Test]
        public void SearchDrivers_NoFreeDriversOnMap()
        {
            var map = new Map(100, 100);
            var coordsClient = new Coordinates(50, 50);
            var algorithm = new RangedManhattanAlgorithm(map);

            List<Driver> drivers = algorithm.SearchDrivers(coordsClient);

            Assert.That(drivers, Is.Empty);
        }
    }
}
