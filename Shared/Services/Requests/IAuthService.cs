using Refit;
using Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Services.Requests
{
    //https://www.milanjovanovic.tech/blog/refit-in-dotnet-building-robust-api-clients-in-csharp
    public interface IAuthService
    {
        [Post(ApiConstants.ApiRoutes.USER_REGENERATE_USER)]
        Task<Guid> GenerateUserAsync();
        [Post(ApiConstants.ApiRoutes.AUTH_LOGIN)]
        Task<TokenDTO> AuthAsync(Guid id);
    }    
}
