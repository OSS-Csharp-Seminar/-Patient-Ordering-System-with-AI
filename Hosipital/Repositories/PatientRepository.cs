using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using DataAccess;

namespace Repositories
{
    public class PatientRepository
    {
        private readonly DatabaseContext _context;

        public PatientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Set<Patient>().FindAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync(string sortBy = null, string filterBy = null)
        {
            var query = _context.Set<Patient>().AsQueryable();

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

        public async Task<Patient> GetByNameAsync(string name)
        {
            return await _context.Set<Patient>().FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task AddAsync(Patient entity)
        {
            await _context.Set<Patient>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Patient> entities)
        {
            await _context.Set<Patient>().AddRangeAsync(entities);
        }

        public void Update(Patient entity)
        {
            _context.Set<Patient>().Update(entity);
        }

        public void Remove(Patient entity)
        {
            _context.Set<Patient>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<Patient> entities)
        {
            _context.Set<Patient>().RemoveRange(entities);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
