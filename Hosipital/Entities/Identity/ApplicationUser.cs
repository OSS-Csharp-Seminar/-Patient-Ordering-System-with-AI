using Microsoft.AspNetCore.Identity;
using Models;


namespace Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] Photo { get; set; }

        public string Oib { get; set; }

        public string IdCardNumber { get; set; }

        public ICollection<Patient> CustomerLoan { get; } = new List<Patient>();

        public ICollection<Patient> LibrarianLoan { get; } = new List<Patient>();
    }
}
