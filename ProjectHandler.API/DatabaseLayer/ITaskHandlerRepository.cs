using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.DatabaseLayer
{
    public interface ITaskHandlerRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();

        Task<TaskItem> GetAsync(int id);

        Task<int> CreateAsync(TaskItem entity);

        Task UpdateAsync(int id, TaskItem entity);

        Task DeleteAsync(TaskItem entity);
    }
}
