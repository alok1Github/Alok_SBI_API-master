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
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserHandler UserHandler;
        private readonly ILogger<UserController> logger;

        public UserController(
            IUserHandler UserHandler,
            ILogger<UserController> logger)
        {
            this.UserHandler = UserHandler;
            this.logger = logger;
        }

        // GET api/User
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                this.logger.LogInformation("Entering into Get all users method");
                return Ok(await UserHandler.GetAllUserAsync());
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Please try again later");
            }
        }

        // GET api/User/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                this.logger.LogInformation($"Getting user detail for {id}");
                return Ok(await UserHandler.GetUserAsync(id));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Issue with server, please try again later");
            }
        }

        // POST api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    this.logger.LogInformation("Provide valid user detail");
                    return BadRequest();
                }

                await UserHandler.AddUserAsync(user);
                this.logger.LogInformation($"User {user.Id} created successfully");

                return Ok($"Task {user.Id} created successfully");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Please try again later");
            }
        }

        // PUT api/User/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            try
            {
                if (user == null || user.Id != id)
                {
                    this.logger.LogInformation("Provide valid user item detail");
                    return BadRequest("Provide a valid task");
                }

                await UserHandler.UpdateUserAsync(id, user);
                this.logger.LogInformation($"Task {user.FirstName + " " + user.LastName} updated successfully");

                return Ok($"Task {user.FirstName + " " + user.LastName} updated successfully");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Please try again later");
            }
        }

        // Delete api/User/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                this.logger.LogInformation($"Getting user detail for {id}");
                await UserHandler.DeleteUserAsync(id);

                return StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Issue with server, please try again later");
            }
        }
    }
}