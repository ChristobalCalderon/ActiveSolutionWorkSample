using CarRental.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CarRental.Domain.Services
{
    public interface IRentService
    {
        Task RentACarAsync(string licensePlate, string ssn, DateTime startOfRent, int startOfCurrentMeter);
        Task ReturnACarAsync(int id, DateTime endOfRent, int endOfCurrentMeter);
    }
}
