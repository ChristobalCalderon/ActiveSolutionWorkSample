using CarRental.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CarRental.Domain.Services
{
    public interface IRentACarService
    {
        Task RentACarAsync(string licensePlate, string ssn, DateTime startOfRent, int startOfCurrentMeter);
        Task ReturnACarAsync(int bookingNr, DateTime endOfRent, int endOfCurrentMeter);
    }
}
