namespace Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public int SpecializationId { get; set; }
        public string Contact { get; set; }
    }
}
