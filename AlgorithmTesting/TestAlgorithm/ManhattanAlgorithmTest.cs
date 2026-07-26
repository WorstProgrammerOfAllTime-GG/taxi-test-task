using maxim_technology_task.Models;
using maxim_technology_task.Services;
using maxim_technology_task.Algorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Testing.TestAlgorithm
{

    [TestFixture]
    public class ManhattanAlgorithmTest
    {
        [Test]
        public void SearchDrivers_ClassicManhattanAlgorithm()
        {

            Map map = new Map(100,100);

            var coordClient = new Coordinates(50,50);

            var closetDriver = new Driver();
            var farDriver = new Driver();

            map.AddDriverCoordinates(90,90, farDriver);
            map.AddDriverCoordinates(60,60, closetDriver);

            var algorithm = new ManhattanAlgorithm(map);

            List<Driver> drivers = algorithm.SearchDrivers(coordClient);

            Assert.That(drivers.Count, Is.EqualTo(2), "Все водители");
            Assert.That(drivers[0], Is.EqualTo(closetDriver), "Ближайший водитель");
            Assert.That(drivers[1], Is.EqualTo(farDriver), "Дальний водитель из возможных");
        }
    }
}
