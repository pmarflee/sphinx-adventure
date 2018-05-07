using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;
using SphinxAdventure.Api.Models;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Queries;
using System;
using System.Threading.Tasks;

namespace SphinxAdventure.Api.Controllers
{
    [Route("api/games")]
    [Authorize]
    public class GameController : Controller
    {
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public GameController(
            IAmACommandProcessor commandProcessor,
            IQueryProcessor queryProcessor)
        {
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        [HttpGet("{id}")]
        public async Task<Game> GetGame(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create()
        {
            var command = new CreateGameCommand(User.Identity.Name);
            await _commandProcessor.SendAsync(command);

            var gameEntity = await _queryProcessor.ExecuteAsync(new GetGameQuery(command.Id));
            var gameModel = new Game
            {
                Id = gameEntity.Id,
                CreatedOn = command.CreatedOn
            };

            return CreatedAtAction("GetGame", new { gameEntity.Id }, gameModel);
        }
    }
}