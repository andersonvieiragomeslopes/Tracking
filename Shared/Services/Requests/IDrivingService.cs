using Refit;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Services.Requests
{
    public interface IDrivingService
    {
        [Post(ApiConstants.ApiRoutes.DRIVING)]
        Task<ApiResponse<RouteResponse>> DrivingAsync(PositionResponse positionResponse);
    }
}
