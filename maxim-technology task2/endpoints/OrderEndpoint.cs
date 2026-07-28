using DeliverySystem.Algorithms;
using DeliverySystem.Exceptions;
using DeliverySystem.Models;
using DeliverySystem.Services;
using maxim_technology_task2.DTO;
using maxim_technology_task2.Services;

namespace maxim_technology_task2.endpoints
{
    public static class OrderEndpoint
    {
        public static void PostCreateOrder(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("/api/user/order/create", async (OrderData orderData, OrderService service, ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("OrderUser");
                try
                {
                    Order order = await service.CreateOrder(RequestFactory.CreateRequest(orderData.ClientID, new Coordinates(orderData.X, orderData.Y)));
                    logger.LogInformation("Заказ {OrderID} был создан : водитель {DriverID} находится на расстоянии {M} и скоро подъедет, ожидайте", order.OrderID, order.DriverID, order.RouteLength);
                    return Results.Ok(order);
                } catch (InvalidCoordinatesException)
                {
                    logger.LogWarning("Клиент {ID} ввел некорректные координаты X:{X}, Y:{Y}",orderData.ClientID, orderData.X, orderData.Y);
                    return Results.BadRequest("Координаты некорректны");
                } catch(DriverNotFoundException)
                {
                    logger.LogInformation("Для клиента {ID} не нашлось свободных водителей", orderData.ClientID);
                    return Results.BadRequest("Свободных водителей нет");
                }            
            });
        }
    }
}
