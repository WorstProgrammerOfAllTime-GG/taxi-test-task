using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task.Models
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
