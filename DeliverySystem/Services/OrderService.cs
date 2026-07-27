using DeliverySystem.Algorithms;
using DeliverySystem.Models;
using DeliverySystem.Services;
using DeliverySystem.Status;
using DeliverySystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverySystem.Services
{
    public class OrderService
    {
        private readonly IAlgorithm _algorithm;
        private readonly Map _map;
       

        public OrderService(Map map, IAlgorithm algorithm)
        {
             Console.WriteLine("Заказ принят в обработку...");
            _map = map;       
            _algorithm = algorithm;
        }
        public Order CreateOrder(RequestOrder requestOrder)
        {
            Console.WriteLine("Идет создание заказа...");
            if (!_map.VerificationValidCoordinates(requestOrder.CoordinatesClient.X, requestOrder.CoordinatesClient.Y))
            {
                Console.WriteLine("Введены неверные координаты");
                throw new InvalidCoordinatesException("Координаты заданы неверно!");
            }
            Console.WriteLine("Координаты валидны.Запуск алгоритма поиска...");
            var drivers = _algorithm.SearchDrivers(requestOrder.CoordinatesClient);
            if (drivers.Count == 0) throw new DriverNotFoundException("Водители не были найдены");
            var selectedDriver = drivers.First();
            selectedDriver.Status = StatusDriver.Busy;
            Console.WriteLine($"Алгоритм нашел водителя {selectedDriver.ID}.Создание финального заказа...");
            Order order = new Order(requestOrder.ClientID, selectedDriver.ID, requestOrder.CoordinatesClient);
            return order;                   
        }
    }
}
