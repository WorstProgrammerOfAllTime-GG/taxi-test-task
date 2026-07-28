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
        private readonly IRandomNumberService _randomNumberService;

        public OrderService(Map map, IAlgorithm algorithm, IRandomNumberService randomNumber)
        {
             Console.WriteLine("Заказ принят в обработку...");
            _map = map;       
            _algorithm = algorithm;
            _randomNumberService = randomNumber;
        }
        public async Task<Order> CreateOrder(RequestOrder requestOrder)
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
            int index;
            try
            {
                int randomNumber = await _randomNumberService.GetRandomNumber();
                index = Math.Abs(randomNumber) % drivers.Count;
            }
            catch
            {
                index = Random.Shared.Next(0, drivers.Count);
            }

            var selectedDriver = drivers[index];
            selectedDriver.Status = StatusDriver.Busy;
            int routeLength = CalculationRouteLength.CalulateRouteLength(requestOrder.CoordinatesClient, selectedDriver.Coordinates);
            Console.WriteLine($"Алгоритм нашел водителя {selectedDriver.ID}.Создание финального заказа...");
            Order order = new Order(requestOrder.ClientID, selectedDriver.ID, requestOrder.CoordinatesClient);
            return order;                   
        }

       
    }
}
