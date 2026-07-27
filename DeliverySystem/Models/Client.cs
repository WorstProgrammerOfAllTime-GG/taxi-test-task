using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Models
{
    public class Client
    {
        public string ID { get; }

        public Client()
        {
            ID = Guid.NewGuid().ToString();
        }

        public RequestOrder CreateReqeustOrder(int x, int y)
        {
            return new RequestOrder(this.ID, new Coordinates(x,y));  
        }
    }
}
