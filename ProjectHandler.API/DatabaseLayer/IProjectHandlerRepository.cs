using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.DatabaseLayer
{
    public interface IProjectHandlerRepository
    {
        Task<IEnumerable<ProjectResponse>> GetAllAsync();

        Task<Project> GetAsync(int id);

        Task<int> CreateAsync(Project entity);

        Task UpdateAsync(int id, Project entity);

        Task DeleteAsync(int id);
    }
}
