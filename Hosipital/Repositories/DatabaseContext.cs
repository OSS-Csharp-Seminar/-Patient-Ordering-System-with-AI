using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specialization - Doctor relationship
            modelBuilder.Entity<Doctor>()
                .Property(d => d.SpecializationId);

            // Doctor - Order relationship
            modelBuilder.Entity<Order>()
                .Property(o => o.DoctorId);

            // Patient - Order relationship
            modelBuilder.Entity<Order>()
                .Property(o => o.PatientId);
        }
    }
}
