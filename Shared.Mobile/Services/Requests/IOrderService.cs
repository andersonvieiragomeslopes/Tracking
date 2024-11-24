using Refit;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Services.Requests
{
    public interface IOrderService
    {
        [Post(Constants.ApiRoutes.ORDER_MY_ORDERS)]
        Task<ApiResponse<List<OrderResponse>>> MyOrdersAsync();
    }
}
