using System.Collections.Generic;

namespace N_Tier.Core.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }

        // Navigation Properties
        public ICollection<Order> Orders { get; set; }
    }
}
