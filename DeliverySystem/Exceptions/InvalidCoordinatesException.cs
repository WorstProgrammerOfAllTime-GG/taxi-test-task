using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Exceptions
{
    public class InvalidCoordinatesException : Exception
    {
        public InvalidCoordinatesException(string message) : base(message) { }       
    }
}
