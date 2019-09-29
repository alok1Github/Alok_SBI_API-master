using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.DatabaseLayer
{
    public class UserHandlerRepository : IUserHandlerRepository
    {
        private readonly TaskHandlerDbContext TaskHandlerDbContext;

        public UserHandlerRepository(TaskHandlerDbContext TaskHandlerDbContext)
        {
            this.TaskHandlerDbContext = TaskHandlerDbContext;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteEntity = TaskHandlerDbContext.User.FirstOrDefaultAsync(s => s.Id == id);

            this.TaskHandlerDbContext.User.Remove(deleteEntity.Result);

            await TaskHandlerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await this.TaskHandlerDbContext.User.AsNoTracking<User>().ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await TaskHandlerDbContext.User.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> CreateAsync(User entity)
        {
            entity.Project = (entity.ProjectId != 0) ?
                TaskHandlerDbContext.Project.FirstOrDefault(x => x.Id == entity.ProjectId) : null;
            entity.Task = (entity.TaskId != 0) ?
                TaskHandlerDbContext.Tasks.FirstOrDefault(x => x.Id == entity.TaskId) : null;
            TaskHandlerDbContext.User.Add(entity);
 
            return await TaskHandlerDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, User entity)
        {
            TaskHandlerDbContext.User.Update(entity);

            await TaskHandlerDbContext.SaveChangesAsync();
        }
    }
}
