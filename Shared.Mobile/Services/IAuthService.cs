using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Services
{
    //https://www.milanjovanovic.tech/blog/refit-in-dotnet-building-robust-api-clients-in-csharp
    public interface IAuthService
    {
        [Post(Constants.ApiRoutes.REGENERATE_USER)]
        Task<Guid> PostGenerateUserAsync();
    }
}
