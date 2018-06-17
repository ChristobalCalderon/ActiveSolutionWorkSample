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

            priceRepository.Setup(x=> 
            x.GetPriceByCategory(CarCategory.SmallCar
            )).Returns(Task.FromResult(new Price
            {
                PerDay = 100,
                PerKm = 2.5,
                CarType = CarCategory.SmallCar
            }));

            priceRepository.Setup(x =>
            x.GetPriceByCategory(CarCategory.Combi
            )).Returns(Task.FromResult(new Price
            {
                PerDay = 200,
                PerKm = 3,
                CarType = CarCategory.Combi
            }));

            priceRepository.Setup(x =>
            x.GetPriceByCategory(CarCategory.Truck
            )).Returns(Task.FromResult(new Price
            {
                PerDay = 300,
                PerKm = 3.5,
                CarType = CarCategory.Truck
            }));

            rentRepository.Setup(x =>
            x.AddAsync(It.IsAny<Models.Rent>())).Returns(Task.FromResult(1));

            rentRepository.Setup(x =>
            x.GetByIdAsync(1)).Returns(Task.FromResult(new Models.Rent()
            {
                Id = 1,
                CarCategory = CarCategory.SmallCar,
                PIN = "871121-0057",
                StartOfRent = DateTime.Parse("2018-06-18"),
                LicensePlate = "MBLC298",
                StartOfCurrentMeter = 100,
            }));

            rentRepository.Setup(x =>
            x.GetByIdAsync(2)).Returns(Task.FromResult(new Models.Rent()
            {
                Id = 2,
                CarCategory = CarCategory.Combi,
                PIN = "871121-0057",
                StartOfRent = DateTime.Parse("2018-06-18"),
                LicensePlate = "MBLC2999",
                StartOfCurrentMeter = 100,
            }));

            rentRepository.Setup(x =>
            x.GetByIdAsync(3)).Returns(Task.FromResult(new Models.Rent()
            {
                Id = 3,
                CarCategory = CarCategory.Truck,
                PIN = "871121-0057",
                StartOfRent = DateTime.Parse("2018-06-18"),
                LicensePlate = "MBLC288",
                StartOfCurrentMeter = 100,
            }));

            rentRepository.Setup(x =>
            x.UpdateAsync(new Models.Rent()
            {
                Id = 1,
                PIN = "871121-0057",
                StartOfRent = DateTime.Parse("2018-06-17"),
                LicensePlate = "MBLC298",
                StartOfCurrentMeter = 100,
                EndOfRent = DateTime.Parse("2018-06-18"),
                EndofCurrentMeter = 200,
                Price = 2000
            })).Returns(Task.CompletedTask);

            rentRepository.Setup(x =>
            x.UpdateAsync(new Models.Rent()
            {
                Id = 2,
                PIN = "871121-0057",
                StartOfRent = DateTime.Parse("2018-06-17"),
                LicensePlate = "MBLC299",
                StartOfCurrentMeter = 100,
                EndOfRent = DateTime.Parse("2018-06-18"),
                EndofCurrentMeter = 200,
                Price = 2000
            })).Returns(Task.CompletedTask);

            rentRepository.Setup(x =>
            x.UpdateAsync(new Models.Rent()
            {
                Id = 3,
                PIN = "871121-0057",
                StartOfRent = DateTime.Parse("2018-06-17"),
                LicensePlate = "MBLC288",
                StartOfCurrentMeter = 100,
                EndOfRent = DateTime.Parse("2018-06-18"),
                EndofCurrentMeter = 200,
                Price = 2000
            })).Returns(Task.CompletedTask);

            return new RentService(rentRepository.Object, priceRepository.Object);
        }
    }
}
