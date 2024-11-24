using Polly;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Mobile.Services
{
    public abstract class RetryPolicyService
    {
        private readonly IAsyncPolicy _authPolicy;
        private readonly IAsyncPolicy _asyncPolicy;
        protected RetryPolicyService() : this(null) { }
        protected RetryPolicyService(int? numberOfRetries)
        {
            _authPolicy = Policy
                .HandleInner<ApiException>(AuthStatus)
                .RetryAsync(Constants.Policy.MAX_REFRESH);
            _asyncPolicy = Policy
                .Handle<ApiException>(StatusCode)
                .WaitAndRetryAsync(numberOfRetries ?? Constants.Policy.MAX_RETRY, SleepDuration);
        }

        protected async Task<T> RequestWithPolicy<T>(Func<Task<T>> func) =>
            await _asyncPolicy.WrapAsync(_authPolicy).ExecuteAsync(func).ConfigureAwait(false);
        protected virtual Task RefreshAuthorizationAsync(Exception error, int attempt) =>
    Task.CompletedTask;
        private static bool StatusCode(ApiException ex) =>
    ex.StatusCode != HttpStatusCode.NotFound && ex.StatusCode != HttpStatusCode.Forbidden && ex.StatusCode != HttpStatusCode.Unauthorized;
        private static bool AuthStatus(ApiException ex) =>
    ex.StatusCode == HttpStatusCode.Unauthorized;
        private static TimeSpan SleepDuration(int attempt) =>
            TimeSpan.FromSeconds(Math.Pow(2, attempt));
    }
}
