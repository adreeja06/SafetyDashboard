namespace _1stModule_PIPremises.Models
{
    public class User
    {
        public int UserID { get; set; }

        public required string EmployeeName { get; set; }

        public required string Flag { get; set; } // "Employee", "Visitor", etc.

        public int LocationID { get; set; }

        public required Location Location { get; set; }

        // ✅ NEW FIELDS:
        public string? Designation { get; set; }

        public string? Remarks { get; set; }

        // ✅ NECESSARY FIX: Navigation property for EF Core
        public List<SwipeLog> SwipeLogs { get; set; } = new();
    }
}
