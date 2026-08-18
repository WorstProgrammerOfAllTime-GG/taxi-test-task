using DeliverySystem.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Models
{
    public class Driver
    {
        public Coordinates Coordinates { get; set; }
        public StatusDriver Status { get; set; }
        public string ID { get; init; }

        public Driver() 
        {            
            ID  = Guid.NewGuid().ToString();
            Status = StatusDriver.Free;
        }
 
    }
}
