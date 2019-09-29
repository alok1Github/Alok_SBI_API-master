using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHandler.API.DatabaseLayer;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.BusinessLayer
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserHandlerRepository UserHandlerRepository;
        private readonly ILogger<UserHandler> logger;

        public UserHandler(IUserHandlerRepository UserHandlerRepository,
            ILogger<UserHandler> logger)
        {
            this.logger = logger;
            this.UserHandlerRepository = UserHandlerRepository;
        }

        public async Task<int> AddUserAsync(User user)
        {
            return await UserHandlerRepository.CreateAsync(user);
        }

        public Task DeleteUserAsync(int id)
        {
            return UserHandlerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await UserHandlerRepository.GetAllAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await UserHandlerRepository.GetAsync(id);
        }

        public async Task UpdateUserAsync(int id, User user)
        {
            await this.UserHandlerRepository.UpdateAsync(id, user);
        }
    }
}
