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
        ///  the personal identity number of a person
        ///  the day they rent the car
        ///  and the current meter on the car when the customer receives the car
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="personalIdentityNumber">Personal identity number</param>
        /// <param name="startOfRent"></param>
        /// <param name="currentMeter"></param>
        /// <returns>Booking number</returns>
        public async Task<int> RentAsync(string licensePlate, string personalIdentityNumber, DateTime startOfRent, int currentMeter)
        {
            var rx = new Regex(@"\b(((20)((0[0-9])|(1[0-1])))|(([1][^0-8])?\d{2}))((0[1-9])|1[0-2])((0[1-9])|(2[0-9])|(3[01]))[-+]?\d{4}[,.]?\b");
            if(!rx.IsMatch(personalIdentityNumber))
            {
                throw new ArgumentException($"RentService::RentACarAsync Invalid SSN : {personalIdentityNumber}");
            }

            Rent rent = new Rent {
                LicensePlate = licensePlate,
                PersonalIdentityNumber = personalIdentityNumber,
                StartOfRent = startOfRent,
                StartOfCurrentMeter = currentMeter
            };

            return await _rentRepository.AddAsync(rent);
        }
        
        /// <summary>
        /// Function takes the rental/booking number,
        /// day the car returns
        /// and what the meter stands on when returning the car
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endOfRent"></param>
        /// <param name="endOfCurrentMeter"></param>
        /// <returns>The rent object</returns>
        public async Task<Rent> ReturnAsync(int id, DateTime endOfRent, int endOfCurrentMeter)
        {
            Rent rent = await _rentRepository.GetByIdAsync(id);

            if(DateTime.Compare(rent.StartOfRent, endOfRent) > 0)
            {
                throw new ArgumentException("RentService::ReturnAsync Invalid date, end date is before start date");
            }

            if (rent.StartOfCurrentMeter > endOfCurrentMeter)
            {
                throw new ArgumentException("RentService::ReturnAsync Current meter is below starting meter");
            }

            rent.EndOfRent = endOfRent;
            rent.EndofCurrentMeter = endOfCurrentMeter;
            Price price = await _priceRepository.GetPriceByCarCategoryAsync(rent.CarCategory);
            rent.Price = rent.CalculateRentalPrice(price.PerDay, price.PerKm);
            await _rentRepository.UpdateAsync(rent);
            return rent; 
        }
    }
}
