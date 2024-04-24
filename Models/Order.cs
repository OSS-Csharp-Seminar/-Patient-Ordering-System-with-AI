namespace HospitalApp.Models
{
    public class Order
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Diagnosis { get; set; }
        public DateTime DateTime { get; set; }
    }

}
