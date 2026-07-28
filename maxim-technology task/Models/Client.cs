using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task.Models
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
