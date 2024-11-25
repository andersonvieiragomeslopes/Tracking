using Refit;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Requests
{
    public interface IAddressService
    {
        [Post(ApiConstants.ApiRoutes.ADDRESS)]
        Task<ApiResponse<AddressResponse>> AddressAsync(double lat, double lon);
    }
}
