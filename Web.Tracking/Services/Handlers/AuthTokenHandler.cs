using Shared;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Net.Http.Headers;

namespace Web.Tracking.Services.Handlers
{
    public class AuthTokenHandler : DelegatingHandler
    {
        public AuthTokenHandler()
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiConstants.JTW_DEBUG);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
