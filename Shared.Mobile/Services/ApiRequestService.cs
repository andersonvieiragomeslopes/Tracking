using Refit;
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
        Task<List<OrderResponse>> MyOrdersAsync();

    }
    public class ApiRequestService : IApiRequestService
    {
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;

        public ApiRequestService(IAuthService authService, IOrderService orderService)
        {
            _authService = authService;
            _orderService = orderService;
        }
        public async Task<TokenDTO> AuthAsync(Guid id) =>
            await _authService.AuthAsync(id);        
        public async Task<Guid> GenerateUserAsync() =>
            await _authService.GenerateUserAsync();        
        
        public async Task<List<OrderResponse>> MyOrdersAsync() =>
            await _orderService.MyOrdersAsync();
        
            
    }
}
