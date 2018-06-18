using CarRental.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Repositories
{
    public interface IPriceRepository : IRepository<Price>
    {
        Task<Price> GetPriceByCarCategoryAsync(CarCategory carCategory); 
    }
}
