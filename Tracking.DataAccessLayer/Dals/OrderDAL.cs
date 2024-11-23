using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccessLayer.Entities;

namespace Tracking.DataAccessLayer.Dals
{
    public interface IOrderDAL
    {
        Task<int> CountAsync();
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetOrderByUserId(Guid userId);
        Task<Order> Get(Guid id);
        Task<Guid> Create(Order entity);
    }
    public class OrderDAL : BaseDAL<Order>, IOrderDAL
    {
        private readonly TrackingContext _context;
        public OrderDAL(TrackingContext context): base(context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await CountTotalAsync();
        }
        public async Task<Order> Get(Guid id)
        {
            try
            {
                return await FindAsync(id);
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
        public async Task<IEnumerable<Order>> GetAll()
        {
            try
            {
                return await SelectAsync();
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
        public async Task<IEnumerable<Order>> GetOrderByUserId(Guid userId)
        {
            try
            {
                return await _context.Orders.Where(x=>x.UserId == userId).ToListAsync();
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
        public async Task<Guid> Create(Order entity)
        {
           return await CreateAsync(entity);
        }
    }
}
