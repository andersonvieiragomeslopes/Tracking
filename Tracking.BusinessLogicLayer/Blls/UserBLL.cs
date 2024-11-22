using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.BusinessLogicLayer.DTOs.ViewModels;
using Tracking.DataAccessLayer.Dals;

namespace Tracking.BusinessLogicLayer.Blls
{
    public interface IUserBLL
    {
        Task<List<UserViewModel>> GetAll();
    }
    public class UserBLL : IUserBLL
    {
        private readonly IUserDAL _userDAL;

        public UserBLL(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }
        public async Task<List<UserViewModel>> GetAll()
        {
            var result = await _userDAL.GetAll();
            return result.Select(x=> new UserViewModel(x.Id, x.CreatedAt, x.UpdatedAt)).ToList();
        }
    }
}
