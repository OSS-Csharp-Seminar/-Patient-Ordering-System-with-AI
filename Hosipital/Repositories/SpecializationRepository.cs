using System.Collections.Generic;
using System.Linq;
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
            return await _context.Set<Specialization>()
                                  .Where(s => s.Name != null) // Filter by non-null Name property
                                  .ToListAsync();
        }


        public async Task AddAsync(Specialization entity)
        {
            await _context.Set<Specialization>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Specialization> entities)
        {
            await _context.Set<Specialization>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public void Update(Specialization entity)
        {
            _context.Set<Specialization>().Update(entity);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(Specialization entity)
        {
            _context.Set<Specialization>().Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<Specialization> entities)
        {
            _context.Set<Specialization>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync(string sortBy, string filterBy)
        {
            IQueryable<Specialization> query = _context.Specializations;

            if (!string.IsNullOrEmpty(filterBy))
            {
                query = query.Where(s => EF.Functions.Like(s.Name, "%" + filterBy + "%")); // Use Like for case-insensitive filtering
            }

            if (sortBy == "name")
            {
                query = query.OrderBy(s => s.Name);
            }
            else if (sortBy == "name_desc")
            {
                query = query.OrderByDescending(s => s.Name);
            }

            var result = await query.ToListAsync();

            // Handle null values after retrieval
            foreach (var specialization in result)
            {
                if (specialization.Name != null)
                {
                    specialization.Name = specialization.Name;  // No change if not null
                }
                else
                {
                    specialization.Name = "";  // Assign empty string if null
                }
            }

            return result;
        }





        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        public void Remove(Specialization entity)
        {
            _context.Remove(entity);
        }
    }




}
