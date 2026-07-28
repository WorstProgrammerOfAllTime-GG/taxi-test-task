using maxim_technology_task.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task.Algorithms
{
    public interface IAlgorithm
    {
        public List<Driver> SearchDrivers(Coordinates coordClient);
    }
}
