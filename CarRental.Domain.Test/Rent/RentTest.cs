using System;
using System.Threading.Tasks;
using Xunit;

namespace CarRental.Domain.Test.Rent
{
    public class RentTest
    {
        //private readonly IRentRepository _rentRepository;
        //private readonly IPriceRepository _priceRepository;

        public RentTest()
        {

        }

        [Fact]
        public async Task Rent_A_Car()
        {
            Func<Task> testCode = () => Task.Factory.StartNew(ThrowingMethod);

            var ex = await Assert.ThrowsAsync<NotImplementedException>(testCode);

            Assert.IsType<NotImplementedException>(ex);
        }

        void ThrowingMethod()
        {
            throw new NotImplementedException();
        }
    }
}
