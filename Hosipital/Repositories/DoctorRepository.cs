using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Models;


namespace Repositories
{

    public class DoctorRepository
    {
        private readonly DatabaseContext _context;

        public DoctorRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Set<Doctor>().FindAsync(id);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync(string sortBy = null, string filterBy = null, string filterBySpecialization = null)
        {
            var query = from doctor in _context.Set<Doctor>()
                        join specialization in _context.Set<Specialization>()
                        on doctor.SpecializationId equals specialization.Id
                        select new
                        {
                            Doctor = doctor,
                            SpecializationName = specialization.Name
                        };

            if (!string.IsNullOrEmpty(filterBy))
            {
                query = query.Where(s => s.Doctor.Name.Contains(filterBy) ||
                                         s.Doctor.Surname.Contains(filterBy) ||
                                         s.Doctor.Contact.Contains(filterBy));
            }

            if (!string.IsNullOrEmpty(filterBySpecialization))
            {
                query = query.Where(s => s.SpecializationName.Contains(filterBySpecialization));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = query.OrderBy(s => s.Doctor.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(s => s.Doctor.Name);
                        break;
                    case "surname":
                        query = query.OrderBy(s => s.Doctor.Surname);
                        break;
                    case "surname_desc":
                        query = query.OrderByDescending(s => s.Doctor.Surname);
                        break;
                    case "specialization":
                        query = query.OrderBy(s => s.SpecializationName);
                        break;
                    case "specialization_desc":
                        query = query.OrderByDescending(s => s.SpecializationName);
                        break;
                    default:
                        query = query.OrderBy(s => s.Doctor.Id);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(s => s.Doctor.Id);
            }

            var result = await query.Select(s => s.Doctor).ToListAsync();
            return result;
        }


        public async Task AddAsync(Doctor entity)
        {
            await _context.Set<Doctor>().AddAsync(entity);
            await _context.SaveChangesAsync(); 
        }

        public async Task AddRangeAsync(IEnumerable<Doctor> entities)
        {
            await _context.Set<Doctor>().AddRangeAsync(entities);
        }

        public void Update(Doctor entity)
        {
            _context.Set<Doctor>().Update(entity);
        }

        public void Remove(Doctor entity)
        {
            _context.Set<Doctor>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<Doctor> entities)
        {
            _context.Set<Doctor>().RemoveRange(entities);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DoctorExistsAsync(string name)
        {
            return await _context.Set<Doctor>().AnyAsync(d => d.Name == name);
        }

        public async Task<Doctor> LoginAsync(string name, string password)
        {
            return await _context.Set<Doctor>().FirstOrDefaultAsync(d => d.Name == name && d.Password == password);
        }

    }
}
