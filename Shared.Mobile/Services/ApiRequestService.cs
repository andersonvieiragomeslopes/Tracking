using Refit;
using Shared.Mobile.Services.Requests;
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

    }
    public class ApiRequestService : IApiRequestService
    {
        private readonly IAuthService _authService;

        public ApiRequestService(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<TokenDTO> AuthAsync(Guid id) =>
            await _authService.AuthAsync(id);        
        public async Task<Guid> GenerateUserAsync() =>
            await _authService.GenerateUserAsync();
        
            
    }
}
