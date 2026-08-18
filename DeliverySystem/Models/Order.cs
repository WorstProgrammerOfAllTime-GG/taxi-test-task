using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Models
{
    public class Order
    {
        public Coordinates DriverCoordinates { get; }
        public string OrderID { get; }
        public string ClientID { get; }
        public string DriverID { get; }
        public int RouteLength { get; set; }
        public List<Coordinates> RouteList { get; set; }


        public Order(string clientID,string driverID, Coordinates coordinates, int routLength, List<Coordinates> roudList)
        {
            OrderID = Guid.NewGuid().ToString();
            if (clientID == null)
            {
                throw new ArgumentException(nameof(clientID),"Непредвиденная работа программы : clientID is null");
            }
            ClientID = clientID;
            DriverID = driverID;
            Console.WriteLine($"Создан заказ {OrderID} от пользователя {ClientID}");
            DriverCoordinates = coordinates;
            RouteLength = routLength;
            RouteList = roudList;
        }
       
    }
}
