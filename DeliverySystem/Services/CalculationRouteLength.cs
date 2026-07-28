using DeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace DeliverySystem.Services
{
    public class CalculationRouteLength
    {

        public static int CalulateRouteLength(Coordinates coordClient, Coordinates coordDriver)
        {
            return Math.Abs(coordClient.X - coordDriver.X) + Math.Abs(coordClient.Y - coordDriver.Y);
        }
    }
}
