using NSubstitute;
using SphinxAdventure.Core.CommandHandlers;
using SphinxAdventure.Core.Commands;
using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Factories;
using SphinxAdventure.Core.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SphinxAdventure.Tests.Core.CommandHandlers
{
    public class CreateGameCommandHandlerAsyncTests
    {
        private readonly IFactory<Map> _mapFactory;
        private readonly IRepository<Game> _gameRepository;
        private readonly CreateGameCommandHandlerAsync _handler;
        private readonly Map _map;
        private readonly CreateGameCommand _command = new CreateGameCommand(Guid.NewGuid());

        public CreateGameCommandHandlerAsyncTests()
        {
            _map = new Map
            {
                Locations = new Dictionary<string, Location>
                {
                    ["default"] = new Location
                    {
                        Description = "Default",
                        Exits = new Dictionary<string, string>(),
                        Items = new List<string>()
                    }
                }
            };

            _mapFactory = Substitute.For<IFactory<Map>>();
            _mapFactory.Create().Returns(_map);

            _gameRepository = Substitute.For<IRepository<Game>>();

            _handler = new CreateGameCommandHandlerAsync(_mapFactory, _gameRepository);
        }

        [Fact]
        public async void ShouldCreateGameInstanceUsingUserIdFromCommand()
        {
            await _handler.HandleAsync(_command);

            await _gameRepository.Received().SaveAsync(
                Arg.Is<Game>(game => game.UserId == _command.UserId));
        }

        [Fact]
        public async void ShouldCreateGameInstanceUsingMapFromRepository()
        {
            await _handler.HandleAsync(_command);

            await _gameRepository.Received().SaveAsync(Arg.Is<Game>(game => game.Map == _map));
        }

        [Fact]
        public async void ShouldCreateGameInstanceThatBeginsInTheFirstLocation()
        {
            await _handler.HandleAsync(_command);

            await _gameRepository.Received().SaveAsync(
                Arg.Is<Game>(game => game.Location == _map.Locations.First().Value));
        }
    }
}
