using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using DataAccess;


namespace Repositories
{
    

    public class SpecializationRepository 
    {
        private readonly DatabaseContext _context;

        public SpecializationRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Specialization> GetByIdAsync(int id)
        {
            return await _context.Set<Specialization>().FindAsync(id);
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _context.Set<Specialization>().ToListAsync();
        }

        public async Task AddAsync(Specialization entity)
        {
            await _context.Set<Specialization>().AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public async Task AddRangeAsync(IEnumerable<Specialization> entities)
        {
            await _context.Set<Specialization>().AddRangeAsync(entities);
        }

        public void Update(Specialization entity)
        {
            _context.Set<Specialization>().Update(entity);
        }

        public void Remove(Specialization entity)
        {
            _context.Set<Specialization>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<Specialization> entities)
        {
            _context.Set<Specialization>().RemoveRange(entities);
        }



        public async Task<IEnumerable<Specialization>> GetAllAsync(string sortBy = null, string filterBy = null)
        {
            var query = _context.Set<Specialization>().AsQueryable();

            if (!string.IsNullOrEmpty(filterBy))
            {
                query = query.Where(s => s.Name.Contains(filterBy));
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

    }
}
