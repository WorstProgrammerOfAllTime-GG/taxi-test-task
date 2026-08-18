using System.Runtime.CompilerServices;
using DeliverySystem.Models;
using maxim_technology_task2.DTO;
using DeliverySystem.Services;

namespace maxim_technology_task2.endpoints
{
    public static class DriverCoordinatesEndpoint
    {
        public static void PutDriverCoordinates(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("/api/drivers/coordinates", (DriverData data, Map map, ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("DriverCoordinates");
                var driver = map.DriverSearchByID(data.ID);

                if (driver is null)
                {
                    if (!map.VerificationValidCoordinates(data.X, data.Y))
                    {
                        logger.LogInformation("Водитель {ID} ввел некорректные координаты X:{X}, Y:{Y}", data.ID, data.X, data.Y);
                        return Results.BadRequest("Координаты некорректны");
                    }

                    if (!map.TryAddDriver(data.ID, new Coordinates(data.X, data.Y), out driver))
                    {
                        logger.LogInformation("Водитель {ID} попытался занять координаты X:{X}, Y:{Y}, но они уже заняты", data.ID, data.X, data.Y);
                        return Results.BadRequest("Здесь уже находится другой водитель");
                    }

                    logger.LogInformation("Водитель {ID} успешно добавил координаты X:{X}, Y:{Y}", driver.ID, data.X, data.Y);
                    return Results.Ok("Координаты успешно добавлены");
                }

                if (!map.VerificationValidCoordinates(data.X, data.Y))
                {
                    logger.LogInformation("Водитель {ID} ввел некорректные координаты X:{X}, Y:{Y}. Старые координаты удалены", driver.ID, data.X, data.Y);
                    map.RemoveOldCoordinates(driver);

                    return Results.BadRequest("Координаты некорректны");
                }
                else if (!map.TryChangeDriverCoordinates(driver, data.X, data.Y))
                {
                    logger.LogInformation("Водитель {ID} ввел уже занятые координаты X:{X}, Y:{Y}", driver.ID, data.X, data.Y);
                    return Results.BadRequest("Здесь уже находится другой водитель");
                }

                logger.LogInformation("Водитель {ID} успешно изменил координаты X:{X}, Y:{Y}", driver.ID, data.X, data.Y);
                return Results.Ok("Координаты успешно изменены");
            });
        }
    }
}
