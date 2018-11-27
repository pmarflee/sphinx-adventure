using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Paramore.Brighter;
using Paramore.Darker;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.DTOs;
using SphinxAdventure.Core.Queries;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SphinxAdventure.Api.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IQueryProcessor _queryProcessor;

        public TokenController(
            IConfiguration configuration,
            IAmACommandProcessor commandProcessor,
            IQueryProcessor queryProcessor)
        {
            _configuration = configuration;
            _commandProcessor = commandProcessor;
            _queryProcessor = queryProcessor;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] GetTokenRequest request)
        {
            var authenticateUserCommand = new AuthenticateUserCommand(
                request.Username, request.Password);

            await _commandProcessor.SendAsync(authenticateUserCommand);

            if (!authenticateUserCommand.IsValidUsernameAndPassword)
            {
                return BadRequest(new { Message = "Invalid username or password" });
            }

            var user = await _queryProcessor.ExecuteAsync(new GetUserByUsernameQuery(request.Username));

            return new ObjectResult(GenerateToken(user));
        }

        private string GenerateToken(Core.DTOs.User user)
        {
            var claims = new Claim[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Username),
                new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
            };

            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)), 
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}