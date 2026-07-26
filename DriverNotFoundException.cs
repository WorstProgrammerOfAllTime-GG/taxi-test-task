using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task
{
    public class DriverNotFoundException : Exception
    {
        public DriverNotFoundException(string message) : base(message) { }
        
    }
}
