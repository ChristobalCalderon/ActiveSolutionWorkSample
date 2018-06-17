using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.ExtensionMethods
{
    public static class RentExtension
    {
        /// <summary>
        /// Default value for small cars and new categories 
        /// </summary>
        /// <param name="rent"></param>
        /// <param name="perDay"></param>
        /// <param name="perKm"></param>
        /// <returns></returns>
        public static double CalculateRentalPrice(this Rent rent, double perDay, double perKm)
        {
            double price = 0d;

            switch (rent.CarCategory)
            {
                case Models.CarCategory.Combi:
                    price = perDay * rent.NrOfDays * 1.3 + perKm * rent.NrOfKm;
                    break;
                case Models.CarCategory.Truck:
                    price = perDay * rent.NrOfDays * 1.5 + perKm * rent.NrOfKm * 1.5;
                    break;
                default:
                    price = perDay * perKm;
                    break;
             }

            return price;
        }
    }
}
