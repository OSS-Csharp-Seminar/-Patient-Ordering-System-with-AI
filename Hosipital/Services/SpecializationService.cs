using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Repositories;


namespace Services
{
    public interface ISpecializationService
    {
        Task<Specialization> GetByIdAsync(int id);
        Task<IEnumerable<Specialization>> GetAllAsync(string sortBy = null, string filterBy = null);
        Task AddAsync(Specialization entity);
        Task AddRangeAsync(IEnumerable<Specialization> entities);
        void Update(Specialization entity);
        void Remove(Specialization entity);
        void RemoveRange(IEnumerable<Specialization> entities);
        Task SaveChangesAsync();

    }

    public class SpecializationService : ISpecializationService
    {
        private readonly SpecializationRepository _repository;

        public SpecializationService(SpecializationRepository repository)
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

        public async Task<IEnumerable<Specialization>> GetAllAsync(string sortBy = null, string filterBy = null)
        {
            return await _repository.GetAllAsync(sortBy, filterBy);
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }


    }
}
