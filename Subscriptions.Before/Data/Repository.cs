using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Subscriptions.Before.SharedKernel;

namespace Subscriptions.Before.Data
{
    public interface IRepository<T> where T: Entity
    {
        Task<T> FindByIdAsync(Guid id);
        Task Insert(T entity);
        Task SaveChangesAsync();
    }
    public class Repository<T>: IRepository<T> where T: Entity
    {
        private readonly SubscriptionContext _context;

        public Repository(SubscriptionContext context)
        {
            _context = context;
        }
        public Task<T> FindByIdAsync(Guid id)
        {
            return _context.Set<T>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public Task Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
    
}