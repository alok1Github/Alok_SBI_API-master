using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectHandler.API.BusinessLayer;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.Features
{
    [Route("api/Project")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectHandler ProjectHandler;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(
            IProjectHandler ProjectHandler,
            ILogger<ProjectController> logger)
        {
            this.ProjectHandler = ProjectHandler;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {        
                return Ok(await ProjectHandler.GetProjectsAsync());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {               
                return Ok(await ProjectHandler.GetProjectAsync(id));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

   
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            try
            {
                if (project == null)
                {
                    this.logger.LogInformation("Provide valid task item detail");
                    return BadRequest();
                }

                await ProjectHandler.AddProjectAsync(project);
                this.logger.LogInformation($"Task {project.Id} created successfully");

                return Ok(project.Id);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Please try again later");
            }
        }

     
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Project project)
        {
            try
            {
                if (project == null || project.Id != id)
                {
                    return BadRequest("Provide a valid project");
                }

                await ProjectHandler.UpdateProjectAsync(id, project);

                this.logger.LogInformation($"Project { project.Name } updated successfully");

                return Ok($"Project { project.Name } updated successfully");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // Delete api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {               
                await ProjectHandler.DeleteAsync(id);

                return StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}