using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.BusinessLogicLayer.DTOs.Records;
using Tracking.BusinessLogicLayer.DTOs.ViewModels;
using Tracking.DataAccessLayer.Dals;
using Tracking.DataAccessLayer.Entities;

namespace Tracking.BusinessLogicLayer.Blls
{
    public interface IUserBLL
    {
        Task<List<UserViewModel>> GetAll();
        Task<Guid> Create();
        Task<UserViewModel> Get(Guid id);
        Task<Guid> Create(UserRecord userRecord);
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
            return result.Select(x=> new UserViewModel(x.Id, x.Name, x.CreatedAt, x.UpdatedAt)).ToList();
        }
        public async Task<UserViewModel> Get(Guid id)
        {
            var result = await _userDAL.Get(id);
            return new UserViewModel(result.Id, result.Name, result.CreatedAt, result.UpdatedAt);
        }
        readonly string[] Names = { "Lennon", "McCartney", "Will Smith", "Harrison Ford", "Elon Musk", "Ainstein", "Bill Gates", "Edward Snowden", "Mr Robot", "FIll Collins" };

        //https://stackoverflow.com/questions/1753508/how-could-i-get-a-random-string-from-a-list-and-assign-it-to-a-variable
        public async Task<Guid> Create()
        {
            var prefix = Guid.NewGuid();
            Random randNum = new Random();
            int aRandomPos = randNum.Next(Names.Count());
            string currentName = Names[aRandomPos];
            return await _userDAL.Create(new User { Name = $"{currentName}{" - "}{prefix}" });
        }
        public async Task<Guid> Create(UserRecord userRecord)
        {
            var prefix = Guid.NewGuid();
            return await _userDAL.Create(new User { Name = $"{userRecord.Name}{" - "}{prefix}" });
        }
    }
}
