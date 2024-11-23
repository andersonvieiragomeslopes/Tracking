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
    public interface IOrderBLL
    {
        Task<List<OrderViewModel>> GetAll();
        Task<List<OrderViewModel>> GetMyOrders();
        Task<OrderViewModel> Get(Guid id);
        Task<Guid> Create(OrderRecord orderRecord);
    }
    public class OrderBLL : IOrderBLL
    {
        private readonly IOrderDAL _orderDAL;
        private readonly IUserBLL _userBLL;
        private readonly IAuthBLL _authBLL;

        public OrderBLL(IOrderDAL orderDAL, IUserBLL userBLL, IAuthBLL authBLL)
        {
            _orderDAL = orderDAL;
            _userBLL = userBLL;
            _authBLL = authBLL;
        }
        public async Task<List<OrderViewModel>> GetAll()
        {
            var result = await _orderDAL.GetAll();
            return result.Select(x => new OrderViewModel(x.Id, x.UserId, x.Title, x.Description, x.Image, x.Latitude, x.Longitude, x.CreatedAt, x.UpdatedAt)).ToList();
        } 
        public async Task<List<OrderViewModel>> GetMyOrders()
        {
            var userLogged = await _authBLL.Logged();
            var result = await _orderDAL.GetAll();
            return result.Select(x => new OrderViewModel(x.Id, x.UserId, x.Title, x.Description, x.Image, x.Latitude, x.Longitude, x.CreatedAt, x.UpdatedAt)).ToList();
        }
        public async Task<OrderViewModel> Get(Guid id)
        {
            var result = await _orderDAL.Get(id);
            return new OrderViewModel(result.Id, result.UserId, result.Title, result.Description, result.Image, result.Latitude, result.Longitude, result.CreatedAt, result.UpdatedAt);
        }
       
        public async Task<Guid> Create(OrderRecord orderRecord)
        {
            var user = await _userBLL.Get(orderRecord.UserId);
            if (user == null)
                throw new Exception("User not found.");
            return await _orderDAL.Create(new Order
            {
                Image = orderRecord.Image,
                Description = orderRecord.Description,
                Title = orderRecord.Title,
                UserId = orderRecord.UserId,
                Longitude = orderRecord.Longitude,
                Latitude = orderRecord.Latitude
            });
        }
    }
}
