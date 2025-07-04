using System.Collections.Generic;

namespace _1stModule_PIPremises.Models
{
    public class Location
    {
        public int LocationID { get; set; }

        public required string LocationName { get; set; }

        public List<User> Users { get; set; } = new(); // ✅ Changed from Person to User
    }
}
