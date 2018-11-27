using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;
using Paramore.Darker;
using SphinxAdventure.Api.Models;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SphinxAdventure.Api.Controllers
{
    [Route("api/games")]
    [Authorize]
    public class GameController : BaseController
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

        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var games = await _queryProcessor.ExecuteAsync(new GetGamesQuery(UserId));

            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGame(Guid id)
        {
            var game = await _queryProcessor.ExecuteAsync(new GetGameQuery(id));

            return Ok(game);
        }

        [HttpPost("")]
        public async Task<IActionResult> Create()
        {
            var command = new CreateGameCommand(UserId);

            await _commandProcessor.SendAsync(command);

            return CreatedAtAction(
                "GetGame", 
                new { command.Id }, 
                new { command.Id, command.CreatedOn });
        }
    }
}