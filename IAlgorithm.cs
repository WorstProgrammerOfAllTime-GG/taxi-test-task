using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task
{
    public interface IAlgorithm
    {
        public List<Driver> SearchDrivers(Coordinates coordClient);
    }
}
