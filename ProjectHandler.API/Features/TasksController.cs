using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectHandler.API.BusinessLayer;
using ProjectHandler.API.Models;

namespace ProjectHandler.API.Features
{
    [Produces("application/json")]
    [Route("api/Tasks")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITaskHandler TaskHandler;
        private readonly ILogger<TasksController> logger;

        public TasksController(
            ITaskHandler TaskHandler,
            ILogger<TasksController> logger)
        {
            this.TaskHandler = TaskHandler;
            this.logger = logger;
        }

        // GET api/Tasks
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                this.logger.LogInformation("Entering into Get all tasks method");
                return Ok(await TaskHandler.GetAllTasksAsync());
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Please try again later");
            }
        }

        // GET api/Tasks/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                this.logger.LogInformation($"Getting task detail for {id}");
                return Ok(await TaskHandler.GetTaskAsync(id));
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Issue with server, please try again later");
            }
        }

        // POST api/Tasks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskItem taskItem)
        {
            try
            {
                if(taskItem == null)
                {
                    this.logger.LogInformation("Provide valid task item detail");
                    return BadRequest();
                }

                await TaskHandler.AddTaskAsync(taskItem);
                this.logger.LogInformation($"Task {taskItem.Id} created successfully");

                return Ok(taskItem.Id);
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Please try again later");
            }
        }

        // PUT api/Tasks/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TaskItem taskItem)
        {
            try
            {
                if (taskItem == null || taskItem.Id != id)
                {
                    this.logger.LogInformation("Provide valid task item detail");
                    return BadRequest("Provide a valid task");
                }

                if (!TaskHandler.IsTaskItemValid(taskItem))
                {
                    this.logger.LogInformation("You can not close this task as it has child tasks");
                    return BadRequest("You can not close this task as it has child tasks");
                }

                await TaskHandler.UpdateTaskAsync(id, taskItem);
                this.logger.LogInformation($"Task {taskItem.Name} updated successfully");

                return Ok($"Task {taskItem.Name} updated successfully");
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Please try again later");
            }
        }
    }
}
