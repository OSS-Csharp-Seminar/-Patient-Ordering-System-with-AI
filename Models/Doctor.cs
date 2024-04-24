namespace HospitalApp.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int SpecializationId { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
    }

}
