using System;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public string Diagnosis { get; set; }
    }
}
