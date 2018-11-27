using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;
using SphinxAdventure.Api.Models;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.DTOs;
using SphinxAdventure.Core.Queries;
using System;
using System.Threading.Tasks;

namespace SphinxAdventure.Api.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public UserController(
            IAmACommandProcessor commandProcessor,
            IQueryProcessor queryProcessor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<User> GetUser(Guid id)
        {
            return await _queryProcessor.ExecuteAsync(new GetUserByIdQuery(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var registerUserCommand = new CreateUserCommand(user.Username, user.Password);

            await _commandProcessor.SendAsync(registerUserCommand);

            if (!registerUserCommand.UserRegistered)
            {
                return BadRequest(new { Message = "User could not be created" });
            }

            var responseObject = new { registerUserCommand.Id };

            return CreatedAtAction("GetUser", responseObject, responseObject);
        }
    }
}