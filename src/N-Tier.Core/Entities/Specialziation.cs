using System.Collections.Generic;

namespace N_Tier.Core.Entities
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
    }
}
