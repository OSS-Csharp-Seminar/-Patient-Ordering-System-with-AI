using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Repositories;


namespace Services
{
    public interface ISpecializationService
    {
        Task<IEnumerable<Specialization>> GetAllAsync(string sortBy = null, string filterBy = null);
        Task<Specialization> GetByIdAsync(int id);
        Task AddAsync(Specialization specialization);
        Task UpdateAsync(Specialization specialization);
        Task RemoveAsync(Specialization specialization);
        Task SaveChangesAsync();
    }


    public class SpecializationService : ISpecializationService
    {
        private readonly SpecializationRepository _repository;

        public SpecializationService(SpecializationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Specialization>> GetAllAsync(string sortBy = null, string filterBy = null)
        {
            return await _repository.GetAllAsync(sortBy, filterBy);
        }

        public async Task<Specialization> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(Specialization specialization)
        {
            await _repository.AddAsync(specialization);
        }

        public async Task UpdateAsync(Specialization specialization)
        {
            _repository.Update(specialization);
        }

        public async Task RemoveAsync(Specialization specialization)
        {
            _repository.Remove(specialization);
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }
    }


}
