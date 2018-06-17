using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.ExtensionMethods
{
    public static class RentExtension
    {
        /// <summary>
        /// Default value is used for small cars and new categories 
        /// </summary>
        /// <param name="rent"></param>
        /// <param name="perDay"></param>
        /// <param name="perKm"></param>
        /// <returns></returns>
        public static double CalculateRentalPrice(this Rent rent, double perDay, double perKm)
        {
            double price = 0d;

            int nrOfDays = DateTime.Compare(rent.StartOfRent, rent.EndOfRent);
            //If customer returns a car the same day, we still need to charge the customer for a day
            nrOfDays = nrOfDays == 0 ? 1 : nrOfDays;
            int nrOfKm = rent.EndofCurrentMeter - rent.StartOfCurrentMeter;

            switch (rent.CarCategory)
            {
                case Models.CarCategory.Combi:
                    price = perDay * nrOfDays * 1.3 + perKm * nrOfKm;
                    break;
                case Models.CarCategory.Truck:
                    price = perDay * nrOfDays * 1.5 + perKm * nrOfKm * 1.5;
                    break;
                default:
                    price = perDay * perKm;
                    break;
             }

            return price;
        }
    }
}
