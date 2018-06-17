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
        public async Task RentCar_NewRental__Success()
        {
            int bookingNumber = await _rentService.RentAsync("MBLC298", "871121-0057", DateTime.Parse("2018-06-17"), 0);

            Assert.True(bookingNumber > 0);
        }

        [Theory]
        [InlineData(1, 100, "2018-06-18")]
        [InlineData(1, 700, "2018-06-25")]
        public async Task ReturnCar_SmallCarPrice_Success(int bookingNumber, double expectedPrice, string returnDayOfCar)
        {
            Rent rent = await _rentService.ReturnAsync(bookingNumber, DateTime.Parse(returnDayOfCar), 200);
            Assert.Equal(CarCategory.SmallCar, rent.CarCategory);
            Assert.Equal(expectedPrice, rent.Price);
        }

        [Theory]
        [InlineData(2, 560, "2018-06-18", 200)]
        [InlineData(2, 5721, "2018-06-27", 1227)]
        public async Task ReturnCar_CombiCarPrice_Success(int bookingNumber, double expectedPrice, string returnDayOfCar, int endOfMeter)
        {
            Rent rent = await _rentService.ReturnAsync(bookingNumber, DateTime.Parse(returnDayOfCar), endOfMeter);
            Assert.Equal(CarCategory.Combi, rent.CarCategory);
            Assert.Equal(expectedPrice, rent.Price);
        }

        [Theory]
        [InlineData(3, 975, "2018-06-18", 200)]
        [InlineData(3, 5550, "2018-06-28", 300)]
        public async Task ReturnCar_TrunkCarPrice_Success(int bookingNumber, double expectedPrice, string returnDayOfCar, int endOfMeter)
        {
            Rent rent = await _rentService.ReturnAsync(bookingNumber, DateTime.Parse(returnDayOfCar), endOfMeter);
            Assert.Equal(CarCategory.Truck, rent.CarCategory);
            Assert.Equal(expectedPrice, rent.Price);
        }

        [Fact]
        public void Rent_InvalidCarCategory_Then_ThrowException()
        {
            var rent = new Rent()
            {
               Id = 1,
               PersonalIdentityNumber = "8711210057",
               CarCategory = CarCategory.InvalidCarCategory,
               StartOfRent = DateTime.Parse("2018-06-17"),
               StartOfCurrentMeter = 100,
               LicensePlate = "MBLC298"
            };

            Assert.Throws<Exception>(() => rent.CalculateRentalPrice(200,200));
        }

        [Fact]
        public async Task RentCar_InvalidPersonalIdentityNumber__Then_ThrowException()
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
    }
}
