using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHandler.API.BusinessLayer
{
    public interface IProjectHandler
    {
        Task<int> AddProjectAsync(Models.Project project);

        Task<IEnumerable<Models.ProjectResponse>> GetProjectsAsync();

        Task<Models.Project> GetProjectAsync(int id);

        Task UpdateProjectAsync(int id, Models.Project project);

        Task DeleteAsync(int id);
    }
}
