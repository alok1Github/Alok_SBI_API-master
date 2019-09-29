using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectHandler.API.DatabaseLayer;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.BusinessLayer
{
    public class TaskHandler : ITaskHandler
    {
        private readonly ITaskHandlerRepository TaskHandlerRepository;
        private readonly ILogger<TaskHandler> logger;

        public TaskHandler(
            ITaskHandlerRepository TaskHandlerRepository,
            ILogger<TaskHandler> logger)
        {
            this.TaskHandlerRepository = TaskHandlerRepository;
            this.logger = logger;
        }

        public async Task<int> AddTaskAsync(TaskItem taskItem)
        {
            return await TaskHandlerRepository.CreateAsync(taskItem);
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await TaskHandlerRepository.GetAllAsync();
        }

        public async Task<TaskItem> GetTaskAsync(int id)
        {
            return await TaskHandlerRepository.GetAsync(id);
        }

        public async Task UpdateTaskAsync(int id, TaskItem taskItem)
        {
            await this.TaskHandlerRepository.UpdateAsync(id, taskItem);
        }

        public bool IsTaskItemValid(TaskItem taskItem)
        {
            this.logger.LogInformation($"checking task is valid to close or not");
            var taskItems = this.TaskHandlerRepository.GetAllAsync().Result;
            var isValid = !taskItems.Any(t => t.ParentTaskId == taskItem.Id && t.EndTask == false);

            var logMessage = (isValid) 
                            ? $"Task {taskItem.Name} is valid to close"
                            : $"Task {taskItem.Name} is not valid to close";

            this.logger.LogInformation(logMessage);

            return isValid;
        }
    }
}
