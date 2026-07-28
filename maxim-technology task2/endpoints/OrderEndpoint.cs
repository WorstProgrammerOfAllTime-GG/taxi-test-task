using maxim_technology_task2.DTO;
using DeliverySystem.Services;
using DeliverySystem.Models;
using DeliverySystem.Algorithms;
using maxim_technology_task2.Services;

namespace maxim_technology_task2.endpoints
{
    public static class OrderEndpoint
    {
        public static void PostCreateOrder(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("/api/user/order/create", async (OrderData orderData, Map map, OrderService service) =>
            {
                Order order = await service.CreateOrder(RequestFactory.CreateRequest(orderData.ClientID, new Coordinates(orderData.X, orderData.Y)));

            });
        }
    }
}
