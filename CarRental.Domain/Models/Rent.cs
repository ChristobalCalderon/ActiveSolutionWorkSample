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
        //personal identity number
        public string PersonalIdentityNumber { get; set; }
        public DateTime StartOfRent { get; set; }
        public DateTime EndOfRent { get; set; }
        public int StartOfCurrentMeter { get; set; }
        public int EndofCurrentMeter { get; set; }
        public CarCategory CarCategory { get; set; }
        public double Price { get; set; }

        /// <summary>
        /// Calculates the price of the rent based on what category the car belongs to
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
                case CarCategory.SmallCar:
                    price = perDay * perKm;
                    break;
                case CarCategory.Combi:
                    price = perDay * nrOfDays * 1.3 + perKm * nrOfKm;
                    break;
                case CarCategory.Truck:
                    price = perDay * nrOfDays * 1.5 + perKm * nrOfKm * 1.5;
                    break;
                default:
                    throw new Exception("Invalid car category and cannot calculate rent price");
            }

            return price;
        }
    }
}
