using System;
using System.Collections.Generic;
using System.Text;

namespace maxim_technology_task
{
    public class Driver
    {
        public Coordinates Coordinates { get; set; }
        public StatusDriver Status { get; set; }
        public string ID { get; }

        public Driver() 
        {            
            ID  = Guid.NewGuid().ToString();
            Status = StatusDriver.Free;
        }
 
    }
}
