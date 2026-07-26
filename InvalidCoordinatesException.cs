using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task
{
    public class InvalidCoordinatesException : Exception
    {
        public InvalidCoordinatesException(string message) : base(message) { }       
    }
}
