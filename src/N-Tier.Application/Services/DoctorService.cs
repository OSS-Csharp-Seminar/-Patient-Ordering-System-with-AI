using System.Collections.Generic;
using System.Threading.Tasks;
using N_Tier.Core.Entities;
using N_Tier.DataAccess.Repositories;

namespace N_Tier.Core.Services
{
    public interface IDoctorService
    {
        Task<Doctor> GetByIdAsync(int id);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task AddAsync(Doctor entity);
        Task AddRangeAsync(IEnumerable<Doctor> entities);
        void Update(Doctor entity);
        void Remove(Doctor entity);
        void RemoveRange(IEnumerable<Doctor> entities);
    }

    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repository;

        public DoctorService(IDoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(Doctor entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Doctor> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public void Update(Doctor entity)
        {
            _repository.Update(entity);
        }

        public void Remove(Doctor entity)
        {
            _repository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Doctor> entities)
        {
            _repository.RemoveRange(entities);
        }
    }
}
