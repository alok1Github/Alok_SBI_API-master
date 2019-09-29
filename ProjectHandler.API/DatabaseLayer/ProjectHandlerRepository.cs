using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHandler.API.Models;
using System.Linq;

namespace ProjectHandler.API.DatabaseLayer
{
    public class ProjectHandlerRepository : IProjectHandlerRepository
    {
        private readonly TaskHandlerDbContext TaskHandlerDbContext;

        public ProjectHandlerRepository(TaskHandlerDbContext TaskHandlerDbContext)
        {
            this.TaskHandlerDbContext = TaskHandlerDbContext;
        }

        public async Task DeleteAsync(Project entity)
        {
            this.TaskHandlerDbContext.Project.Remove(entity);

            await TaskHandlerDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProjectResponse>> GetAllAsync()
        {
            var response = from project in TaskHandlerDbContext.Project
                           join user in TaskHandlerDbContext.User on project.Id equals user.ProjectId into usr
                           join task in TaskHandlerDbContext.Tasks on project.Id equals task.ProjectId into tsk
                           from u in usr.DefaultIfEmpty()
                           select new ProjectResponse()
                           {
                               Id = project.Id,
                               Name = project.Name,
                               Priority = project.Priority,
                               StartDate = project.StartDate,
                               EndDate = project.EndDate,
                               UserId = u != null ? u.Id : 0,
                               TaskCount = tsk.Count(),
                               User = u != null ? new User { Id = u.Id, FirstName = u.FirstName, LastName = u.LastName } : null
                           };

            return await response.ToListAsync();
        }

        public async Task<Project> GetAsync(int id)
        {
            return await TaskHandlerDbContext.Project.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> CreateAsync(Project entity)
        {
            TaskHandlerDbContext.Project.Add(entity);

            return await TaskHandlerDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Project entity)
        {
            TaskHandlerDbContext.Project.Update(entity);

            await TaskHandlerDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var deleteEntity = TaskHandlerDbContext.Project.FirstOrDefault(x => x.Id == id);

            TaskHandlerDbContext.Project.Remove(deleteEntity);

            await TaskHandlerDbContext.SaveChangesAsync();
        }
    }
}