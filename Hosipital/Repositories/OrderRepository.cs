using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using DataAccess;

namespace Repositories
{
    public class OrderRepository
    {
        private readonly DatabaseContext _context;

        public OrderRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Set<Order>().FindAsync(id);
        }


        public async Task<IEnumerable<Order>> GetAllAsync(
            string patientName = null,
            string patientSurname = null,
            string doctorName = null,
            string doctorSurname = null,
            string doctorSpecialization = null,
            DateTime? dateOfAppointment = null,
            string sortBy = null)
        {
            var query = from order in _context.Set<Order>()
                        join doctor in _context.Set<Doctor>() on order.DoctorId equals doctor.Id
                        join patient in _context.Set<Patient>() on order.PatientId equals patient.Id
                        join specialization in _context.Set<Specialization>() on doctor.SpecializationId equals specialization.Id
                        select new
                        {
                            Order = order,
                            DoctorName = doctor.Name,
                            DoctorSurname = doctor.Surname,
                            DoctorSpecialization = specialization.Name,
                            PatientName = patient.Name,
                            PatientSurname = patient.Surname,
                            order.DateOfAppointment
                        };

            if (!string.IsNullOrEmpty(patientName))
            {
                query = query.Where(q => q.PatientName.Contains(patientName));
            }

            if (!string.IsNullOrEmpty(patientSurname))
            {
                query = query.Where(q => q.PatientSurname.Contains(patientSurname));
            }

            if (!string.IsNullOrEmpty(doctorName))
            {
                query = query.Where(q => q.DoctorName.Contains(doctorName));
            }

            if (!string.IsNullOrEmpty(doctorSurname))
            {
                query = query.Where(q => q.DoctorSurname.Contains(doctorSurname));
            }

            if (!string.IsNullOrEmpty(doctorSpecialization))
            {
                query = query.Where(q => q.DoctorSpecialization.Contains(doctorSpecialization));
            }

            if (dateOfAppointment.HasValue)
            {
                query = query.Where(q => q.DateOfAppointment.Date == dateOfAppointment.Value.Date);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "patientname":
                        query = query.OrderBy(q => q.PatientName);
                        break;
                    case "patientname_desc":
                        query = query.OrderByDescending(q => q.PatientName);
                        break;
                    case "patientsurname":
                        query = query.OrderBy(q => q.PatientSurname);
                        break;
                    case "patientsurname_desc":
                        query = query.OrderByDescending(q => q.PatientSurname);
                        break;
                    case "doctorname":
                        query = query.OrderBy(q => q.DoctorName);
                        break;
                    case "doctorname_desc":
                        query = query.OrderByDescending(q => q.DoctorName);
                        break;
                    case "doctorsurname":
                        query = query.OrderBy(q => q.DoctorSurname);
                        break;
                    case "doctorsurname_desc":
                        query = query.OrderByDescending(q => q.DoctorSurname);
                        break;
                    case "doctorspecialization":
                        query = query.OrderBy(q => q.DoctorSpecialization);
                        break;
                    case "doctorspecialization_desc":
                        query = query.OrderByDescending(q => q.DoctorSpecialization);
                        break;
                    case "date":
                        query = query.OrderBy(q => q.DateOfAppointment);
                        break;
                    case "date_desc":
                        query = query.OrderByDescending(q => q.DateOfAppointment);
                        break;
                    default:
                        query = query.OrderBy(q => q.Order.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(q => q.Order.Id);
            }

            var result = await query.Select(q => q.Order).ToListAsync();
            return result;
        }


        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Set<Order>().ToListAsync();
        }

        public async Task AddAsync(Order entity)
        {
            await _context.Set<Order>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Order> entities)
        {
            await _context.Set<Order>().AddRangeAsync(entities);
        }

        public void Update(Order entity)
        {
            _context.Set<Order>().Update(entity);
        }

        public void Remove(Order entity)
        {
            _context.Set<Order>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<Order> entities)
        {
            _context.Set<Order>().RemoveRange(entities);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
