using maxim_technology_task.Models;
using maxim_technology_task.Services;
using maxim_technology_task.Algorithms;
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

            var closeDistanceDriver = new Driver();
            var farDistanceDriver = new Driver();

            map.AddDriverCoordinates(45,36, closeDistanceDriver);
            map.AddDriverCoordinates(84,92, farDistanceDriver);

            var algorithm = new RangedManhattanAlgorithm(map);

            List<Driver> drivers = algorithm.SearchDrivers(coordsClient);
            Assert.That(drivers.Count, Is.EqualTo(1));
            Assert.That(drivers[0], Is.EqualTo(closeDistanceDriver));
        }
    }
}
