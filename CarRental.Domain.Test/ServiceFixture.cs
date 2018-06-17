using CarRental.Domain.Models;
using CarRental.Domain.Repositories;
using CarRental.Domain.Services;
using Moq;
using System;
using System.Threading.Tasks;

namespace CarRental.Domain.Test
{
    public class ServiceFixture
    {
        public IRentService Init()
        {
            var rentRepository = new Mock<IRentRepository>();
            var priceRepository = new Mock<IPriceRepository>();
            rentRepository.Setup(x => 
            x.AddAsync(new Models.Rent()
            {
                PIN = "871121-0057",
                StartOfRent = DateTime.Parse("2018-06-17"),
                LicensePlate = "MBLC298",
                StartOfCurrentMeter = 0,
            })).Returns(Task.FromResult(1));
           return new RentService(rentRepository.Object, priceRepository.Object);
        }
    }
}
