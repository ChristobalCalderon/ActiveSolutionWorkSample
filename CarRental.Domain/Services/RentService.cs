using CarRental.Domain.Models;
using CarRental.Domain.Repositories;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarRental.Domain.Services
{
    /// <summary>
    /// Service that handles the rental of a car
    /// </summary>
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        private readonly IPriceRepository _priceRepository;

        public RentService(IRentRepository rentRepository, IPriceRepository priceRepository)
        {
            _rentRepository = rentRepository;
            _priceRepository = priceRepository;
        }

        /// <summary>
        ///  Function takes the license plat that is a unique identifier of the car
        ///  the Social Security Number of a person
        ///  the day they rent the car
        ///  and the current meter on the car when the customer receives the car
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="pin">Personal identity number</param>
        /// <param name="startOfRent"></param>
        /// <param name="currentMeter"></param>
        /// <returns>Booking number</returns>
        public async Task<int> RentAsync(string licensePlate, string pin, DateTime startOfRent, int currentMeter)
        {
            var rx = new Regex(@"\b(((20)((0[0-9])|(1[0-1])))|(([1][^0-8])?\d{2}))((0[1-9])|1[0-2])((0[1-9])|(2[0-9])|(3[01]))[-+]?\d{4}[,.]?\b");
            if(!rx.IsMatch(pin))
            {
                throw new ArgumentException($"RentService::RentACarAsync Invalid SSN : {pin}");
            }

            Rent rent = new Rent {
                LicensePlate = licensePlate,
                PIN = pin,
                StartOfRent = startOfRent,
                StartOfCurrentMeter = currentMeter
            };

            return await _rentRepository.AddAsync(rent);
        }
        
        /// <summary>
        /// Function takes the rental/booking number,
        /// day the customer returns the car
        /// and what the meter stands on when returning the car
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endOfRent"></param>
        /// <param name="endOfCurrentMeter"></param>
        /// <returns>The rent object</returns>
        public async Task<Rent> ReturnAsync(int id, DateTime endOfRent, int endOfCurrentMeter)
        {
            Rent rent = await _rentRepository.GetByIdAsync(id);

            if(rent.StartOfRent > endOfRent)
            {
                throw new ArgumentException("RentService::ReturnAsync Invalid date, end date is before start date");
            }

            if (rent.StartOfCurrentMeter > endOfCurrentMeter)
            {
                throw new ArgumentException("RentService::ReturnAsync Current meter is below starting meter");
            }

            rent.EndOfRent = endOfRent;
            rent.EndofCurrentMeter = endOfCurrentMeter;
            Price price = await _priceRepository.GetPriceByCategory(rent.CarCategory);
            rent.Price = rent.CalculateRentalPrice(price.PerDay, price.PerKm);
            await _rentRepository.UpdateAsync(rent);
            return rent; 
        }
    }
}
