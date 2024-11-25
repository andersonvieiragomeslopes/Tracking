using Refit;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Requests
{
    public interface IUserService
    {
        [Get(ApiConstants.ApiRoutes.USERS)]
        Task<ApiResponse<List<UserResponse>>> UsersAsync();
    }
}
