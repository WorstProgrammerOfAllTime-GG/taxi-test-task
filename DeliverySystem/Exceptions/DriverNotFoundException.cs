using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Exceptions
{
    public class DriverNotFoundException : Exception
    {
        public DriverNotFoundException(string message) : base(message) { }
        
    }
}
