using CarRental.Domain.ExtensionMethods;
using CarRental.Domain.Models;
using CarRental.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace CarRental.Domain.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class RentACarService : IRentACarService
    {
        private readonly IRentRepository _rentRepository;
        private readonly IPriceRepository _priceRepository;

        public RentACarService(IRentRepository carRepo, IPriceRepository priceRepo)
        {
            _rentRepository = carRepo;
            _priceRepository = priceRepo;
        }

        /// <summary>
        //  
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="ssn"></param>
        /// <param name="startOfRent"></param>
        /// <param name="currentMeter"></param>
        /// <returns></returns>
        public async Task RentACarAsync(string licensePlate, string ssn, DateTime startOfRent, int currentMeter)
        {
            Rent rent = new Rent {
                LicensePlate = licensePlate,
                SSN = ssn,
                StartOfRent = startOfRent,
                StartOfCurrentMeter = currentMeter
            };
            await _rentRepository.AddAsync(rent);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="endOfRent"></param>
        /// <param name="endOfCurrentMeter"></param>
        /// <returns></returns>
        public async Task ReturnACarAsync(int id, DateTime endOfRent, int endOfCurrentMeter)
        {
            Rent rent = await _rentRepository.GetByIdAsync(id);
            rent.EndOfRent = endOfRent;
            rent.EndofCurrentMeter = endOfCurrentMeter;
            Price price = await _priceRepository.GetPriceByCategory(rent.CarCategory);
            rent.Price = rent.CalculateRentalPrice(price.PerDay, price.PerKm);
            await _rentRepository.UpdateAsync(rent);
        }
    }
}
