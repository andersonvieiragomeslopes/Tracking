using Refit;
using Shared.Mobile.Repositories;
using Shared.Mobile.Services.Requests;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Services
{
    public interface IApiRequestService
    {
        Task<TokenDTO> AuthAsync(Guid id);
        Task<Guid> GenerateUserAsync();
        Task<IEnumerable<OrderResponse>> MyOrdersAsync();

    }
    public class ApiRequestService : IApiRequestService
    {

        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public ApiRequestService(IAuthService authService, IOrderService orderService, IOrdersRepository ordersRepository)
        {
            _authService = authService;
            _orderService = orderService;
            _ordersRepository = ordersRepository;
        }
        public async Task<TokenDTO> AuthAsync(Guid id) =>
            await _authService.AuthAsync(id);        
        public async Task<Guid> GenerateUserAsync() =>
            await _authService.GenerateUserAsync();        
        
        public async Task<IEnumerable<OrderResponse>> MyOrdersAsync()
        {                     
            var response = await _orderService.MyOrdersAsync();
            return response;
            //await _ordersRepository.SaveAllAsync(response);
            //return await _ordersRepository.GetAllAsync();
        }
            
        
            
    }
}
