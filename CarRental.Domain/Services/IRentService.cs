using CarRental.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CarRental.Domain.Services
{
    public interface IRentService
    {
        Task<int> RentAsync(string licensePlate, string personalIdentityNumber, DateTime startOfRent, int startOfCurrentMeter);
        Task<Rent> ReturnAsync(int id, DateTime endOfRent, int endOfCurrentMeter);
    }
}
