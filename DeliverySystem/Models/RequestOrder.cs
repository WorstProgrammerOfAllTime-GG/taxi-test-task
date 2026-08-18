using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Models
{
    public class RequestOrder
    {
        public string ClientID { get; }
        public Coordinates CoordinatesClient { get; }

        public RequestOrder(string clientID, Coordinates coordClient)
        {
            ClientID = clientID; CoordinatesClient = coordClient;
        }
    }
}
