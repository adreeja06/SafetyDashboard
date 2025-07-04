namespace _1stModule_PIPremises.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public required string Name { get; set; }
        public required string Flag { get; set; }

        // ✅ Add this for navigation — EF will use it
        public List<SwipeLog> SwipeLogs { get; set; } = new();
    }
}
