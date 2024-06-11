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

        public async Task<IEnumerable<Doctor>> GetAllAsync(string sortBy = null, string filterBy = null)
        {
            var query = _context.Set<Doctor>().AsQueryable();

            if (!string.IsNullOrEmpty(filterBy))
            {
                query = query.Where(s => s.Name.Contains(filterBy) || s.Surname.Contains(filterBy) || s.Contact.Contains(filterBy));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        query = query.OrderBy(s => s.Name);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(s => s.Name);
                        break;

                    case "surname":
                        query = query.OrderBy(s => s.Surname);
                        break;
                    case "surname_desc":
                        query = query.OrderByDescending(s => s.Surname);
                        break;
                    case "specialization":
                        query = query.OrderBy(s => s.SpecializationId);
                        break;
                    case "specialization_desc":
                        query = query.OrderByDescending(s => s.SpecializationId);
                        break;
                    default:
                        query = query.OrderBy(s => s.Id);
                        break;

                }
            }
            else
            {
                query = query.OrderBy(s => s.Id);
            }

            return await query.ToListAsync();
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
