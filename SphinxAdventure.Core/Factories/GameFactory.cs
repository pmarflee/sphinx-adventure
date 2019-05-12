using SphinxAdventure.Core.Entities;
using SphinxAdventure.Core.Infrastructure.Utils;
using System;

namespace SphinxAdventure.Core.Factories
{
    public class GameProps
    {
        public GameProps(Guid id, Guid userId, DateTime createdOn, string location = null)
        {
            Id = id;
            UserId = userId;
            CreatedOn = createdOn;
            Location = location;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public DateTime CreatedOn { get; }
        public string Location { get; }
    }

    public class GameFactory : IFactory<Game, GameProps>
    {
        private readonly IFactory<Map, MapProps> _mapFactory;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public GameFactory(
            IFactory<Map, MapProps> mapFactory, 
            IRandomNumberGenerator randomNumberGenerator)
        {
            _mapFactory = mapFactory;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public Game Create(GameProps props)
        {
            return new Game(
                props.Id, 
                props.UserId, 
                _mapFactory.Create(default), 
                props.CreatedOn,
                props.Location, 
                _randomNumberGenerator.Next);
        }
    }
}
