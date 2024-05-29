using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N_Tier.Core.Entities;

namespace N_Tier.DataAccess.Repositories
{
    

    public class PatientRepository : IPatientRepository
    {
        private readonly DbContext _context;

        public PatientRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Set<Patient>().FindAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _context.Set<Patient>().ToListAsync();
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
    }
}
