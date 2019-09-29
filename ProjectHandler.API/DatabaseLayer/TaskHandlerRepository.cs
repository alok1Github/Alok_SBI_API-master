using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHandler.API.Models;
using System.Linq;

namespace ProjectHandler.API.DatabaseLayer
{
    public class TaskHandlerRepository : ITaskHandlerRepository
    {
        private readonly TaskHandlerDbContext TaskHandlerDbContext;

        public TaskHandlerRepository(TaskHandlerDbContext TaskHandlerDbContext)
        {
            this.TaskHandlerDbContext = TaskHandlerDbContext;
        }

        public async Task DeleteAsync(TaskItem entity)
        {
            this.TaskHandlerDbContext.Tasks.Remove(entity);

            await TaskHandlerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            var response = from task in TaskHandlerDbContext.Tasks
                           join user in TaskHandlerDbContext.User on task.Id equals user.TaskId into usr
                           from u in usr.DefaultIfEmpty()
                           select new TaskItemResponse
                           {
                               Id = task.Id,
                               Name = task.Name,
                               ParentTaskId = task.ParentTaskId,
                               EndDate = task.EndDate,
                               StartDate = task.StartDate,
                               EndTask = task.EndTask,
                               Priority = task.Priority,
                               Project = task.Project != null ? new Project { Id = task.Project.Id, Name = task.Project.Name } : null,
                               ProjectId = task.ProjectId,
                               FirstName = u != null ? u.FirstName : null,
                               LastName = u != null ? u.LastName : null,
                               UserId = u != null ? u.Id : 0,
                               User = u != null ? new User { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName } : null
                           };

            return await response.ToListAsync();
        }

        public async Task<TaskItem> GetAsync(int id)
        {
            return await TaskHandlerDbContext.Tasks.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> CreateAsync(TaskItem entity)
        {
            entity.Project = (entity.ProjectId != 0) ?
                TaskHandlerDbContext.Project.FirstOrDefault(x => x.Id == entity.ProjectId) : null;
            TaskHandlerDbContext.Tasks.Add(entity);

            return await TaskHandlerDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TaskItem entity)
        {
            TaskHandlerDbContext.Tasks.Update(entity);

            await TaskHandlerDbContext.SaveChangesAsync();
        }
    }
}