using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N_Tier.Core.Entities;

namespace N_Tier.DataAccess.Repositories
{
    
    public class DoctorRepository 
    {
        private readonly DbContext _context;

        public DoctorRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Set<Doctor>().FindAsync(id);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Set<Doctor>().ToListAsync();
        }

        public async Task AddAsync(Doctor entity)
        {
            await _context.Set<Doctor>().AddAsync(entity);
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
    }
}
