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
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiOWQyMWZhYTItMjg3MC00NTNiLWIzM2UtYWRkZDcwNGRhZGVhIiwiZXhwIjoxNzM1MDc0Mjg3fQ.pFnh9YwiNjSm80fgPUpRXcuSjjSk4EysEu9FfirTEcQ");
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
