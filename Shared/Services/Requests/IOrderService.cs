﻿using Refit;
using Shared.Models.DTOs.Records;
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
        [Get(ApiConstants.ApiRoutes.ORDERS + "/{id}")]
        Task<ApiResponse<OrderResponse>> GetOrderAsync([AliasAs("id")] Guid id);        
        
        [Post(ApiConstants.ApiRoutes.ORDER_MY_ORDERS)]
        Task<ApiResponse<List<OrderResponse>>> MyOrdersAsync();
        [Get(ApiConstants.ApiRoutes.ORDERS)]
        Task<ApiResponse<List<OrderResponse>>> OrdersAsync();
        [Post(ApiConstants.ApiRoutes.ORDERS)]
        Task<ApiResponse<Guid>> CreateOrderAsync(OrderRecord orderRecord);
    }
}
