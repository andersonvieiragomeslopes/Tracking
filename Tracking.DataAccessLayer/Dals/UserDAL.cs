using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccessLayer.Entities;

namespace Tracking.DataAccessLayer.Dals
{
    public interface IUserDAL
    {
        Task<int> CountAsync();
        Task<IEnumerable<User>> GetAll();
        Task<User> Get(Guid id);
        Task<Guid> Create(User user);
    }
    public class UserDAL : BaseDAL<User>, IUserDAL
    {
        public UserDAL(TrackingContext context): base(context)
        {
        }
        public async Task<int> CountAsync()
        {
            return await CountTotalAsync();
        }
        public async Task<User> Get(Guid id)
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
        public async Task<IEnumerable<User>> GetAll()
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
        public async Task<Guid> Create(User user)
        {
           return await CreateAsync(user);
        }
    }
}
