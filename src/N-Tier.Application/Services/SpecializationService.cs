using System.Collections.Generic;
using System.Threading.Tasks;
using N_Tier.Core.Entities;
using N_Tier.DataAccess.Repositories;

namespace N_Tier.Core.Services
{
    public interface ISpecializationService
    {
        Task<Specialization> GetByIdAsync(int id);
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task AddAsync(Specialization entity);
        Task AddRangeAsync(IEnumerable<Specialization> entities);
        void Update(Specialization entity);
        void Remove(Specialization entity);
        void RemoveRange(IEnumerable<Specialization> entities);
    }

    public class SpecializationService : ISpecializationService
    {
        private readonly ISpecializationRepository _repository;

        public SpecializationService(ISpecializationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Specialization> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(Specialization entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Specialization> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public void Update(Specialization entity)
        {
            _repository.Update(entity);
        }

        public void Remove(Specialization entity)
        {
            _repository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Specialization> entities)
        {
            _repository.RemoveRange(entities);
        }
    }
}
