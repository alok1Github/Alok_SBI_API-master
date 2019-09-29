using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.DatabaseLayer
{
    public interface IUserHandlerRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetAsync(int id);

        Task<int> CreateAsync(User entity);

        Task UpdateAsync(int id, User entity);

        Task DeleteAsync(int id);
    }
}
