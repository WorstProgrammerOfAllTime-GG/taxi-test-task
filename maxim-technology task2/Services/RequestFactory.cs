using DeliverySystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Services
{
    public class RequestFactory
    {
        public static RequestOrder CreateRequest(string id, Coordinates coordinates)
        {
            return new RequestOrder(id, coordinates);
        }
    }
}
