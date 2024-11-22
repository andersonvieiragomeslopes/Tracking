using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccessLayer.Entities;

namespace Tracking.DataAccessLayer.Dals
{
    public abstract class BaseDAL<T> : IDisposable
        where T : ModelBase, new()
    {
        protected readonly TrackingContext _context;
        protected DbSet<T> _dbSet;

        public BaseDAL(TrackingContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual async Task<Guid> CreateAsync(T obj)
        {
            try
            {
                var entity = await _dbSet.AddAsync(obj);
                var id = entity.Entity.Id;
                await _context.SaveChangesAsync();
                return id;
            }
            catch (DbUpdateException error)
            {
                throw new DbUpdateException(error.Message);
            }
            catch (Exception error)
            {
                throw new Exception("An error occurred.", error);
            }
        }
        public virtual async Task<int> CountTotalAsync()
        {
            return await _dbSet.CountAsync();
        }
        public virtual async Task<IList<T>> SelectAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
