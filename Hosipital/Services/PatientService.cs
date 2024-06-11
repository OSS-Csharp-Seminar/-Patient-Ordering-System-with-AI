using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public interface IPatientService
    {
        Task<Patient> GetByIdAsync(int id);
        Task<IEnumerable<Patient>> GetAllAsync(string sortBy = null, string filterBy = null);
        Task AddAsync(Patient entity);
        Task AddRangeAsync(IEnumerable<Patient> entities);
        void Update(Patient entity);
        void Remove(Patient entity);
        void RemoveRange(IEnumerable<Patient> entities);
        Task<Patient> LoginAsync(string name, string password);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }

    public class PatientService : IPatientService
    {
        private readonly PatientRepository _repository;

        public PatientService(PatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Patient>> GetAllAsync(string sortBy = null, string filterBy = null)
        {
            return await _repository.GetAllAsync(sortBy, filterBy);
        }

        public async Task AddAsync(Patient entity)
        {
            await _repository.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Patient> entities)
        {
            await _repository.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public void Update(Patient entity)
        {
            _repository.Update(entity);
            SaveChangesAsync().Wait();
        }

        public void Remove(Patient entity)
        {
            _repository.Remove(entity);
            SaveChangesAsync().Wait();
        }

        public void RemoveRange(IEnumerable<Patient> entities)
        {
            _repository.RemoveRange(entities);
            SaveChangesAsync().Wait();
        }

        public async Task<Patient> LoginAsync(string name, string password)
        {
            var patient = await _repository.GetByNameAsync(name);
            if (patient != null && patient.Password == password)
            {
                return patient;
            }
            return null;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            return patient != null;
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }
    }
}
