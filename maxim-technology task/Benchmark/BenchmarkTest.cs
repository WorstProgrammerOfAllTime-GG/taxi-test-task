using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Disassemblers;
using DeliverySystem.Algorithms;
using DeliverySystem.Models;
using System;
using DeliverySystem;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Benchmark
{
    [MemoryDiagnoser]
    public class BenchmarkTest
    {
        private DeliverySystem.Services.Map _map = null!;
        private Coordinates _client = new(50, 50);

        private ManhattanAlgorithm _manhattan = null!;
        private RangedManhattanAlgorithm _ranged = null!;
        private GridSearchAlgorithm _grid = null!;
        private SectorAlgorithm _sector = null!;

        [GlobalSetup]
        public void Setup()
        {
            _map = new DeliverySystem.Services.Map(1000, 1000);

            Random random = new Random(1);

            for (int i = 0; i < 50000; i++)
            {
                var driver = new Driver();

                int x = random.Next(0, 1000);
                int y = random.Next(0, 1000);

                _map.AddDriverCoordinates(x, y, driver);
            }

            _manhattan = new ManhattanAlgorithm(_map);
            _ranged = new RangedManhattanAlgorithm(_map);
            _grid = new GridSearchAlgorithm(_map);
            _sector = new SectorAlgorithm(_map);
        }


        [Benchmark]
        public void Manhattan()
        {
            _manhattan.SearchDrivers(_client);
        }

        [Benchmark]
        public void RangedManhattan()
        {
            _ranged.SearchDrivers(_client);
        }

        [Benchmark]
        public void Grid()
        {
            _grid.SearchDrivers(_client);
        }

        [Benchmark]
        public void Sector()
        {
            _sector.SearchDrivers(_client);
        }
    }
}
