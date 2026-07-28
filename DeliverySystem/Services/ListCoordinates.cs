using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DeliverySystem.Models;

namespace DeliverySystem.Services
{
    public class ListCoordinates
    {
        public static List<Coordinates> CalculateListCoordinates(Coordinates coordClient, Coordinates coordDriver)
        {
            List<Coordinates> route = new List<Coordinates>();
            int currentX = coordDriver.X;
            int currentY = coordDriver.Y;

            route.Add(new Coordinates(currentX, currentY));

            while (currentX != coordClient.X)
            {
                currentX += (currentX < coordClient.X) ? 1 : -1;
                route.Add(new Coordinates(currentX, currentY));
            }

            while (currentY != coordClient.Y)
            {
                currentY += (currentY < coordClient.Y) ? 1 : -1;
                route.Add(new Coordinates(currentX, currentY));
            }
            return route;
        }
    }
}
