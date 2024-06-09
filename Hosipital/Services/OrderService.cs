using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Repositories;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order entity);
        Task AddRangeAsync(IEnumerable<Order> entities);
        void Update(Order entity);
        void Remove(Order entity);
        void RemoveRange(IEnumerable<Order> entities);
        Task<IEnumerable<DateTime>> GetAvailableTimeSlotsAsync();
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }

    public class OrderService : IOrderService
    {
        private readonly OrderRepository _repository;

        public OrderService(OrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(Order entity)
        {
            await _repository.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Order> entities)
        {
            await _repository.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public void Update(Order entity)
        {
            _repository.Update(entity);
            SaveChangesAsync().Wait();
        }

        public void Remove(Order entity)
        {
            _repository.Remove(entity);
            SaveChangesAsync().Wait();
        }

        public void RemoveRange(IEnumerable<Order> entities)
        {
            _repository.RemoveRange(entities);
            SaveChangesAsync().Wait();
        }

        public async Task<IEnumerable<DateTime>> GetAvailableTimeSlotsAsync()
        {
            var now = DateTime.Now.Date;
            var startOfDay = now.AddHours(8); // Start time (8:00 AM)
            var endOfDay = now.AddHours(16); // End time (4:00 PM)
            var currentTime = DateTime.Now;

            // Get all existing orders for today
            var orders = await _repository.GetAllAsync();

            // Calculate available time slots
            var availableSlots = new List<DateTime>();
            var currentSlot = startOfDay;

            while (currentSlot < endOfDay)
            {
                if (currentTime < currentSlot) // Check if the slot is in the future
                {
                    // Check if the current slot is not within any existing order time
                    if (!orders.Any(o => o.DateOfAppointment.Date == now && o.DateOfAppointment == currentSlot))
                    {
                        availableSlots.Add(currentSlot);
                    }
                }

                // Move to the next 30-minute interval
                currentSlot = currentSlot.AddMinutes(30);
            }

            return availableSlots;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            return order != null;
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }
    }
}
