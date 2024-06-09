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

        // Add constructor that accepts DbContextOptions
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specialization - Doctor relationship
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany(s => s.Doctors)
                .HasForeignKey(d => d.SpecializationId);

            // Doctor - Order relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Doctor)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DoctorId);

            // Patient - Order relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Patient)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PatientId);
        }
    }
}
