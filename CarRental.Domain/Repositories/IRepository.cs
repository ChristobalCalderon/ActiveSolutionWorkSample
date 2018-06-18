using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<int> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
    }
}
