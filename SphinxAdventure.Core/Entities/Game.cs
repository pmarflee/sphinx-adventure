using Newtonsoft.Json;
using System;

namespace SphinxAdventure.Core.Entities
{
    public class Game
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userid")]
        public Guid UserId { get; set; }

        [JsonProperty("createdon")]
        public DateTime CreatedOn { get; set; }
    }
}
