using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHandler.API.BusinessLayer
{
    public interface IUserHandler
    {
        Task<int> AddUserAsync(Models.User user);

        Task<IEnumerable<Models.User>> GetAllUserAsync();

        Task<Models.User> GetUserAsync(int id);

        Task UpdateUserAsync(int id, Models.User user);

        Task DeleteUserAsync(int id);
    }
}
