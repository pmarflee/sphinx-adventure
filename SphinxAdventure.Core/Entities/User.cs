using Newtonsoft.Json;
using System;

namespace SphinxAdventure.Core.Entities
{
    public class User
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("createdon")]
        public DateTime CreatedOn { get; set; }
    }
}
