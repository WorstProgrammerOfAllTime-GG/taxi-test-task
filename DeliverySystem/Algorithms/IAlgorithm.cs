using DeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Algorithms
{
    public interface IAlgorithm
    {
        public List<Driver> SearchDrivers(Coordinates coordClient);
    }
}
