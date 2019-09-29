using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.BusinessLayer
{
    public interface ITaskHandler
    {
        Task<int> AddTaskAsync(TaskItem taskItem);

        Task<IEnumerable<TaskItem>> GetAllTasksAsync();

        Task<TaskItem> GetTaskAsync(int id);

        Task UpdateTaskAsync(int id, TaskItem taskItem);

        bool IsTaskItemValid(TaskItem taskItem);
    }
}
