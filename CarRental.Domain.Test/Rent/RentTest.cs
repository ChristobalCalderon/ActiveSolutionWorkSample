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
    public class RentTest : IClassFixture<ServiceFixture>
    {
        private readonly IRentService _rentService;

        public RentTest(ServiceFixture serviceFixture)
        {
            _rentService = serviceFixture.Init();
        }

        [Fact]
        public async Task RentCar_NewRental__Then_Success()
        {
            int bookingNumber = await _rentService.RentAsync("MBLC298", "871121-0057", DateTime.Parse("2018-06-17"), 0);

            Assert.True(bookingNumber > 0);
        }

        [Fact]
        public async Task RentCar_InvalidPIN__Then_ThrowException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _rentService.RentAsync("DMK129", "831018-0367", DateTime.Parse("2018-06-16"), 0));
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
