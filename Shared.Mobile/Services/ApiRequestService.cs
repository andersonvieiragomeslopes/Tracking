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
        Task<ApiRequestResponse<IEnumerable<OrderResponse>>> MyOrdersAsync();

    }
    public class ApiRequestService : RetryPolicyService, IApiRequestService
    {
        private int GeneralCacheHours = 10;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IOrderService _orderService;
        private readonly IAuthService _authService;
        private readonly ICacheService _cacheService;
        public ApiRequestService(IAuthService authService, IOrderService orderService, IOrdersRepository ordersRepository, ICacheService cacheService)
        {
            _authService = authService;
            _orderService = orderService;
            _ordersRepository = ordersRepository;
            _cacheService = cacheService;
        }
        public async Task<TokenDTO> AuthAsync(Guid id) =>
            await _authService.AuthAsync(id);        
        public async Task<Guid> GenerateUserAsync() =>
            await _authService.GenerateUserAsync();        
        
        public async Task<ApiRequestResponse<IEnumerable<OrderResponse>>> MyOrdersAsync()
        {
            var cacheKey = Constants.ApiRoutes.ORDER_MY_ORDERS;
            if (_cacheService.Exists(cacheKey))
            {
                //await _ordersRepository.SaveAllAsync(response);
                //return await _ordersRepository.GetAllAsync();
                return new ApiRequestResponse<IEnumerable<OrderResponse>>(true, _cacheService.GetCollection<OrderResponse>(cacheKey));
            }
            return await RequestWithPolicy(async () =>
            {
                var response = await _orderService.MyOrdersAsync();
                if (response.IsSuccessStatusCode)
                    _cacheService.Set(cacheKey, response.Content, GeneralCacheHours);
                return new ApiRequestResponse<IEnumerable<OrderResponse>>(response.IsSuccessStatusCode, response?.Content);
            });
        }
    }
}
