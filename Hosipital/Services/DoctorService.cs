using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{

    public interface IDoctorService
    {
        Task<Doctor> GetByIdAsync(int id);
        Task<IEnumerable<Doctor>> GetAllAsync(string sortBy = null, string filterBy = null, string filterBySpecialization = null);
        Task<Doctor> AddAsync(Doctor entity);
        void Update(Doctor entity);
        void Remove(Doctor entity);
        Task SaveChangesAsync();
        Task<Doctor> RegisterAsync(Doctor doctor);
        Task<Doctor> LoginAsync(string name, string password);
        Task<bool> DoctorExistsAsync(string name);
    }


    public class DoctorService : IDoctorService
    {

        private readonly DoctorRepository _repository;

        public DoctorService(DoctorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync(string sortBy = null, string filterBy = null, string filterBySpec = null)
        {
            return await _repository.GetAllAsync(sortBy, filterBy, filterBySpec);
        }

        public async Task<Doctor> AddAsync(Doctor entity)
        {
            await _repository.AddAsync(entity);
            return entity;
        }

        public void Update(Doctor entity)
        {
            _repository.Update(entity);
        }

        public void Remove(Doctor entity)
        {
            _repository.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public async Task<Doctor> RegisterAsync(Doctor doctor)
        {
            await _repository.AddAsync(doctor);
            await _repository.SaveChangesAsync();
            return doctor;
        }

        public async Task<Doctor> LoginAsync(string name, string password)
        {
            return await _repository.LoginAsync(name, password);
        }

        public async Task<bool> DoctorExistsAsync(string name)
        {
            return await _repository.DoctorExistsAsync(name);
        }


    }
}
