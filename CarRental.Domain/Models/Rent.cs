using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRental.Domain.Models
{
    public class Rent
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string PersonalIdentityNumber { get; set; }
        public DateTime StartOfRent { get; set; }
        public DateTime EndOfRent { get; set; }
        public int StartOfCurrentMeter { get; set; }
        public int EndofCurrentMeter { get; set; }
        public CarCategory CarCategory { get; set; }
        public double Price { get; set; }

        /// <summary>
        /// Default value is used for small cars and new categories 
        /// </summary>
        /// <param name="rent"></param>
        /// <param name="perDay"></param>
        /// <param name="perKm"></param>
        /// <returns></returns>
        public double CalculateRentalPrice(double perDay, double perKm)
        {
            double price = 0d;

            int nrOfDays = DateTime.Compare(StartOfRent, EndOfRent);
            //If customer returns a car the same day, we still need to charge the customer for a day
            nrOfDays = nrOfDays == 0 ? 1 : nrOfDays;
            int nrOfKm = EndofCurrentMeter - StartOfCurrentMeter;

            switch (CarCategory)
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
