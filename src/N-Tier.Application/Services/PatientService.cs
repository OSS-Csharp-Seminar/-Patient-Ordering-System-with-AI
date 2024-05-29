using System.Collections.Generic;
using System.Threading.Tasks;
using N_Tier.Core.Entities;
using N_Tier.DataAccess.Repositories;

namespace N_Tier.Core.Services
{
    public interface IPatientService
    {
        Task<Patient> GetByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllAsync();
        Task AddAsync(Patient entity);
        Task AddRangeAsync(IEnumerable<Patient> entities);
        void Update(Patient entity);
        void Remove(Patient entity);
        void RemoveRange(IEnumerable<Patient> entities);
    }

    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(Patient entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Patient> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public void Update(Patient entity)
        {
            _repository.Update(entity);
        }

        public void Remove(Patient entity)
        {
            _repository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Patient> entities)
        {
            _repository.RemoveRange(entities);
        }
    }
}
