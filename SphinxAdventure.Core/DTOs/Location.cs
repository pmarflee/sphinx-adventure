using System.Collections.Generic;

namespace SphinxAdventure.Core.DTOs
{
    public class Location
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Exit> Exits { get; set; }

        public List<Item> Items { get; set; }
    }
}
