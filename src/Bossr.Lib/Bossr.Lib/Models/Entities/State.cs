using System;

namespace Bossr.Lib.Models.Entities
{
    public class StateDto
    {
        public RaidDto Raid { get; set; }
        public DateTime ExpectedMin { get; set; }
        public DateTime ExpectedMax { get; set; }
        public int MissedRaids { get; set; }
    }
}
