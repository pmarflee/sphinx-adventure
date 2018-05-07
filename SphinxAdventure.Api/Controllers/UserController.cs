using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;
using SphinxAdventure.Api.Models;
using SphinxAdventure.Core.Commands;
using System;
using System.Threading.Tasks;

namespace SphinxAdventure.Api.Controllers
{
    [Route("api/users")]
    public class UserController : Controller
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
        public async Task<User> GetUser(string username)
        {
            throw new NotImplementedException();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var registerUserCommand = new RegisterUserCommand(user.Username, user.Password);

            await _commandProcessor.SendAsync(registerUserCommand);

            if (!registerUserCommand.UserRegistered)
            {
                return BadRequest(new { Message = "User could not be registered" });
            }

            return CreatedAtAction("GetUser", new { registerUserCommand.Id },
                new User
                {
                    Id = registerUserCommand.Id,
                    Username = user.Username,
                    CreatedOn = registerUserCommand.CreatedOn 
                });
        }
    }
}