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
    }
    public class UserDAL : IUserDAL
    {
        private readonly TrackingContext _context;
        public UserDAL(TrackingContext context)
        {
            _context = context;
        }
        public async Task<int> CountAsync()
        {
            return await _context.Users
                .CountAsync();
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.Users
                    .ToListAsync();
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
    }
}
