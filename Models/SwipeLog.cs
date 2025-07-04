using System;

namespace _1stModule_PIPremises.Models
{
    public class SwipeLog
    {
        public int SwipeLogId { get; set; }

        // FK for User
        public int UserID { get; set; }

        // FK for Location
        public int LocationID { get; set; }

        public DateTime SwipeIN { get; set; }
        public DateTime? SwipeOUT { get; set; }

        public required string Flag { get; set; }

        // Navigation Properties
        public required User User { get; set; }
        public required Location Location { get; set; }
    }
}
