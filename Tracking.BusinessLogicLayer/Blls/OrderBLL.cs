using Microsoft.AspNetCore.SignalR;
using Shared.Models.DTOs.Records;
using Shared.Models.DTOs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.BusinessLogicLayer.Hubs;
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
        private readonly IHubContext<OrderHub> _hubContext;
        public OrderBLL(IOrderDAL orderDAL, IUserBLL userBLL, IAuthBLL authBLL, IHubContext<OrderHub> hubContext)
        {
            _orderDAL = orderDAL;
            _userBLL = userBLL;
            _authBLL = authBLL;
            _hubContext = hubContext;
        }
        public async Task<List<OrderViewModel>> GetAll()
        {
            var result = await _orderDAL.GetAll();
            return result.Select(x => new OrderViewModel(x.Id, x.UserId, x.Title, x.Description, x.Address, x.Latitude, x.Longitude, x.CreatedAt, x.UpdatedAt)).OrderByDescending(x=>x.UpdatedAt).ToList();
        } 
        public async Task<List<OrderViewModel>> GetMyOrders()
        {
            var userLogged = await _authBLL.Logged();
            var result = await _orderDAL.GetOrderByUserId(userLogged.Id);
            return result.Select(x => new OrderViewModel(x.Id, x.UserId, x.Title, x.Description, x.Address, x.Latitude, x.Longitude, x.CreatedAt, x.UpdatedAt)).ToList();
        }
        public async Task<OrderViewModel> Get(Guid id)
        {
            var result = await _orderDAL.Get(id);
            if (result == null)
                throw new Exception("Order not found.");
            return new OrderViewModel(result.Id, result.UserId, result.Title, result.Description, result.Address, result.Latitude, result.Longitude, result.CreatedAt, result.UpdatedAt);
        }
       
        public async Task<Guid> Create(OrderRecord orderRecord)
        {
            var user = await _userBLL.Get(orderRecord.UserId);
            if (user == null)
                throw new Exception("User not found.");
            var order = new Order
            {
                Address = orderRecord.Address,
                Description = orderRecord.Description,
                Title = orderRecord.Title,
                UserId = orderRecord.UserId,
                Longitude = orderRecord.Longitude,
                Latitude = orderRecord.Latitude
            };


                
            var id = await _orderDAL.Create(order);
            await _hubContext.Clients.All.SendAsync("NewOrder", id);
            return id;
        }
    }
}
