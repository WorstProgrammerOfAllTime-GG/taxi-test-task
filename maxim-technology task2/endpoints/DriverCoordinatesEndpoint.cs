using System.Runtime.CompilerServices;
using maxim_technology_task2.DTO;
using DeliverySystem.Services;

namespace maxim_technology_task2.endpoints
{
    public static class DriverCoordinatesEndpoint
    {
        public static void PutDriverCoordinates (this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("/api/drivers/coordinates", (DriverData data, Map map)=>
            {
                var driver = map.DriverSearchByID(data.ID);
                if (driver is null)
                {
                    if (!map.VerificationValidCoordinates(data.Coordinates.X, data.Coordinates.Y))
                    {
                        return Results.BadRequest("Координаты некорректны");
                    }
                    if (!map.TryAddDriver(data.ID, data.Coordinates, out driver))
                    {
                        return Results.BadRequest("Здесь уже находится другой водитель");
                    }

                    return Results.Ok("Координаты успешно добавлены");
                }
                if (!map.VerificationValidCoordinates(data.Coordinates.X, data.Coordinates.Y))
                {
                    map.RemoveOldCoordinates(driver);
                    return Results.BadRequest("Координаты некорректны");
                } else if (!map.TryChangeDriverCoordinates(driver,data.Coordinates.X, data.Coordinates.Y))
                {
                    return Results.BadRequest("Здесь уже находится другой водитель");
                }
                return Results.Ok("Координаты успешно изменены");          
            });
        }
    }
}
