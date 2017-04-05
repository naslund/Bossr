using System;
using System.Collections.Generic;
using System.Text;

namespace BossrScraper.Models
{
    public class World : IWorld
    {
        public string Name { get; set; }
        public string PlayersOnline { get; set; }
        public string Location { get; set; }
        public string PvpType { get; set; }
        public string Tags { get; set; }
    }
}
