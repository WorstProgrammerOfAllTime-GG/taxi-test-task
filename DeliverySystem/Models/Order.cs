using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Models
{
    public class Order
    {
        public Coordinates Coordinates { get; }
        public string ID { get; }
        public string ClientID { get; }
        public string DriverID { get; }
        
        public Order(string clientID,string driverID, Coordinates coordinates)
        {
            ID = Guid.NewGuid().ToString();
            if (clientID == null)
            {
                throw new ArgumentException(nameof(clientID),"Непредвиденная работа программы : clientID is null");
            }
            ClientID = clientID;
            DriverID = driverID;
            Console.WriteLine($"Создан заказ {ID} от пользователя {ClientID}");
            Coordinates = coordinates;
            Console.WriteLine($"Координаты заказа {ID} установлены : X:{Coordinates.X} и Y:{Coordinates.Y}, водитель {DriverID} выехал");
        }
       
    }
}
