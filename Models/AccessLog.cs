namespace _1stModule_PIPremises.Models
{
    public class AccessLog
    {
        public int LogID { get; set; }
        public int UserID { get; set; }
        public DateTime? SwipeIN { get; set; }
        public DateTime? SwipeOUT { get; set; }
        public int LocationID { get; set; }

        public required User User { get; set; }
    }
}
