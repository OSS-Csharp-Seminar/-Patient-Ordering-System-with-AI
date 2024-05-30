using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N_Tier.Core.Entities;

namespace N_Tier.DataAccess.Repositories
{
    

    public class SpecializationRepository 
    {
        private readonly DbContext _context;

        public SpecializationRepository(DbContext context)
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
    }
}
