using Microsoft.AspNetCore.Identity;
using N_Tier.Core.Entities;
using N_Tier.Core.Entities.Identity;
using N_Tier.Core.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace N_Tier.DataAccess.Persistence
{
    public static class DatabaseContextSeed
    {
        public static async Task SeedDatabaseAsync(DatabaseContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // Seed Roles
            if (!roleManager.Roles.Any())
            {
                foreach (var name in Enum.GetNames(typeof(ApplicationRole)))
                {
                    await roleManager.CreateAsync(new IdentityRole(name));
                }
            }

            // Seed Users
            if (!userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin",
                    DateOfBirth = new DateTime(1980, 1, 1),
                    Oib = "00000000000",
                    IdCardNumber = "000000000"
                };

                await userManager.CreateAsync(admin, "Admin123.?");
                await userManager.AddToRoleAsync(admin, ApplicationRole.Administrator.ToString());

                var doctor = new ApplicationUser
                {
                    UserName = "doctor@hospital.com",
                    Email = "doctor@hospital.com",
                    EmailConfirmed = true,
                    FirstName = "Doctor",
                    LastName = "Doctor",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Oib = "11111111111",
                    IdCardNumber = "111111111"
                };

                await userManager.CreateAsync(doctor, "Doctor123.?");
                await userManager.AddToRoleAsync(doctor, ApplicationRole.Doctor.ToString());

                var patient = new ApplicationUser
                {
                    UserName = "patient@hospital.com",
                    Email = "patient@hospital.com",
                    EmailConfirmed = true,
                    FirstName = "Patient",
                    LastName = "Patient",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    Oib = "22222222222",
                    IdCardNumber = "222222222"
                };

                await userManager.CreateAsync(patient, "Patient123.?");
                await userManager.AddToRoleAsync(patient, ApplicationRole.Patient.ToString());
            }

            // Seed Specializations
            if (!context.Specializations.Any())
            {
                var specializations = new[]
                {
                    new Specialization { Name = "Cardiology" },
                    new Specialization { Name = "Neurology" },
                    new Specialization { Name = "Pediatrics" }
                };

                await context.Specializations.AddRangeAsync(specializations);
                await context.SaveChangesAsync();
            }

            // Seed Doctors
            if (!context.Doctors.Any())
            {
                var cardiology = context.Specializations.First(s => s.Name == "Cardiology");
                var neurology = context.Specializations.First(s => s.Name == "Neurology");

                var doctors = new[]
                {
                    new Doctor { Name = "John", Surname = "Doe", Password = "hashed_password", SpecializationId = cardiology.Id, Contact = "john.doe@hospital.com" },
                    new Doctor { Name = "Jane", Surname = "Smith", Password = "hashed_password", SpecializationId = neurology.Id, Contact = "jane.smith@hospital.com" }
                };

                await context.Doctors.AddRangeAsync(doctors);
                await context.SaveChangesAsync();
            }

            // Seed Patients
            if (!context.Patients.Any())
            {
                var patients = new[]
                {
                    new Patient { Name = "Alice", Surname = "Brown", Password = "hashed_password", Contact = "alice.brown@gmail.com" },
                    new Patient { Name = "Bob", Surname = "Johnson", Password = "hashed_password", Contact = "bob.johnson@gmail.com" }
                };

                await context.Patients.AddRangeAsync(patients);
                await context.SaveChangesAsync();
            }

            // Seed Orders
            if (!context.Orders.Any())
            {
                var doctor = context.Doctors.First();
                var patient = context.Patients.First();

                var orders = new[]
                {
                    new Order { DoctorId = doctor.Id, PatientId = patient.Id, DateOfAppointment = DateTime.Now, Diagnosis = "Flu" }
                };

                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
            }
        }
    }
}
