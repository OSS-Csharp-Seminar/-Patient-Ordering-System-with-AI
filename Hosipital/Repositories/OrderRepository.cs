using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repositories
{
    public class OrderRepository
    {
        private readonly DbContext _context;

        public OrderRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Set<Order>().FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Set<Order>().ToListAsync();
        }

        public async Task AddAsync(Order entity)
        {
            await _context.Set<Order>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Order> entities)
        {
            await _context.Set<Order>().AddRangeAsync(entities);
        }

        public void Update(Order entity)
        {
            _context.Set<Order>().Update(entity);
        }

        public void Remove(Order entity)
        {
            _context.Set<Order>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<Order> entities)
        {
            _context.Set<Order>().RemoveRange(entities);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
