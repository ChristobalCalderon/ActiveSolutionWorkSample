using CarRental.Domain.Repositories;
using CarRental.Domain.Services;
using CarRental.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Domain.Test.Rent
{
    public class RentTest
    {
        private readonly IRentService _rentService;

        public RentTest()
        {
            var rentRepository = new Mock<IRentRepository>();
            var priceRepository = new Mock<IPriceRepository>();
            rentRepository.Setup(x => x.AddAsync(new Models.Rent() {
                Id = 1,
                SSN = "871121-0057",
                StartOfRent = DateTime.Parse("2018-06-17"),
                LicensePlate = "MBLC298",
                StartOfCurrentMeter = 0,
                CarCategory = CarCategory.SmallCar
            }));
            _rentService = new RentService(rentRepository.Object, priceRepository.Object);
        }

        [Fact]
        public async Task Rent_A_CarAsync_Success()
        {
            var result = _rentService.RentACarAsync("DMK129","8711210057",DateTime.Parse("2018-06-17"),0);

            await result;

            Assert.True(result.IsCompleted);
        }

        [Fact]
        public async Task Rent_A_CarAsync_Invalid_SSN()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _rentService.RentACarAsync("DMK129", "831018-0367", DateTime.Parse("2018-06-16"), 0));
        }

        //[Fact]
        //public async Task Rent_A_CarAsync()
        //{
        //    var result = _rentService.RentACarAsync("DMK129", "6911050337", DateTime.Parse("2018-06-16"), 0);

        //    await result;

        //    Assert.True(result.IsCompleted);
        //}
    }
    }
