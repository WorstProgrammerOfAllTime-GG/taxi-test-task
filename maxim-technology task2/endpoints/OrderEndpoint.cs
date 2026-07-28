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
            routeBuilder.MapPost("/api/user/order/create", async (OrderData orderData, Map map, OrderService service) =>
            {
                try
                {
                    Order order = await service.CreateOrder(RequestFactory.CreateRequest(orderData.ClientID, new Coordinates(orderData.X, orderData.Y)));
                    return Results.Ok(order);
                } catch (InvalidCoordinatesException)
                {
                    return Results.BadRequest("Координаты некорректны");
                } catch(DriverNotFoundException)
                {
                    return Results.BadRequest("Свободных водителей нет");
                }            
            });
        }
    }
}
