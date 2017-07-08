using System;

namespace Bossr.Lib.Models.Entities
{
    public class State
    {
        public IRaid Raid { get; set; }
        public DateTime ExpectedMin { get; set; }
        public DateTime ExpectedMax { get; set; }
        public int MissedRaids { get; set; }
    }
}
