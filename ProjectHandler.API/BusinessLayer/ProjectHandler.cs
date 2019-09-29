using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHandler.API.DatabaseLayer;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.BusinessLayer
{
    public class ProjectHandler : IProjectHandler
    {
        private readonly IProjectHandlerRepository ProjectHandlerRepository;
        private readonly ILogger<ProjectHandler> logger;

        public ProjectHandler(IProjectHandlerRepository ProjectHandlerRepository,
            ILogger<ProjectHandler> logger)
        {
            this.logger = logger;
            this.ProjectHandlerRepository = ProjectHandlerRepository;
        }

        public async Task<int> AddProjectAsync(Project project)
        {
            return await ProjectHandlerRepository.CreateAsync(project);
        }

        public async Task<IEnumerable<ProjectResponse>> GetProjectsAsync()
        {
            return await ProjectHandlerRepository.GetAllAsync();
        }

        public async Task<Project> GetProjectAsync(int id)
        {
            return await ProjectHandlerRepository.GetAsync(id);
        }

        public async Task UpdateProjectAsync(int id, Project project)
        {
            await this.ProjectHandlerRepository.UpdateAsync(id, project);
        }

        public async Task DeleteAsync(int id)
        {
            await this.ProjectHandlerRepository.DeleteAsync(id);
        }
    }
}
