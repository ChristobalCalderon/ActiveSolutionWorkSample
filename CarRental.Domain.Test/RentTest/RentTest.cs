using CarRental.Domain.Models;
using CarRental.Domain.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Domain.Test.RentTest
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

        [Fact]
        public async Task ReturnCar_EndMeterIsBelowStartMeter__Then_ThrowException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _rentService.ReturnAsync(1, DateTime.Parse("2018-06-18"), 50));
        }

        [Fact]
        public async Task ReturnCar_EndDateIsBeforeStartDate__Then_ThrowException()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await _rentService.ReturnAsync(1, DateTime.Parse("2018-06-16"), 200));
        }

        [Fact]
        public async Task ReturnCar_RentShouldHaveAPriceNow_Success()
        {
            Rent rent = await _rentService.ReturnAsync(1, DateTime.Parse("2018-06-18"), 200);
            Assert.True(rent.Price > 0);
        }

        [Fact]
        public async Task ReturnCar_SmallCarPrice_Success()
        {
            Rent rent = await _rentService.ReturnAsync(1, DateTime.Parse("2018-06-18"), 200);
            Assert.True(rent.Price == 250);
        }

        [Fact]
        public async Task ReturnCar_CombiCarPrice_Success()
        {
            Rent rent = await _rentService.ReturnAsync(2, DateTime.Parse("2018-06-18"), 200);
            Assert.True(rent.Price == 560);
        }
        [Fact]
        public async Task ReturnCar_TrunkCarPrice_Success()
        {
            Rent rent = await _rentService.ReturnAsync(3, DateTime.Parse("2018-06-18"), 200);
            Assert.True(rent.Price == 975);
        }
    }
}
