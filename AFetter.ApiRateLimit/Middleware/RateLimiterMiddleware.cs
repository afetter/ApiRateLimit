using AFetter.ApiRateLimiter.Model;
using AFetter.ApiRateLimiter.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AFetter.ApiRateLimiter.Middleware
{
    public class RateLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RateLimitSettings _configuration;
        private readonly IMemoryCacheStore _requestStore;
        private readonly IRequestRules _requestValidator;
        
        public RateLimiterMiddleware(RequestDelegate next, 
            IMemoryCacheStore requestStore,
            IRequestRules requestValidator,
            IOptions<RateLimitSettings> configuration)
        {
            _next = next;
            _requestStore = requestStore;
            _requestValidator = requestValidator;
            _configuration = configuration.Value;
        }

        public async Task Invoke(HttpContext context)
        {

            if (_configuration == null || _configuration.Limit == 0)
            {
                await _next.Invoke(context);
                return;
            }

            var requestKey = ResolveIdentify(context);
            var counter = await _requestStore.GetAsync(requestKey);
            var isWindowExpired = _requestValidator.IsSessionExpired(counter.Timestamp, _configuration.ExpireTimeInSeconds);
            var isCountValid = _requestValidator.IsRequestCountValid(counter.Count, _configuration.Limit);

            if (!isCountValid && !isWindowExpired)
            {
                await ReturnTooManyRequestsResponse(context, requestKey);
                return;
            }

            if (isWindowExpired)
            {
                counter.Init();
                await _requestStore.SetAsync(requestKey, counter);
            }

            await ProcessRequest(context, requestKey);
        }

        public string ResolveIdentify(HttpContext context)
        {
            return $"{context.Connection.RemoteIpAddress?.ToString()}-{context.Request.Method}-{context.Request.Path}";
        }

        private async Task ReturnTooManyRequestsResponse(HttpContext context, string requestKey)
        {
            var counter = await _requestStore.GetAsync(requestKey);

            var remaingTime = _requestValidator.GetRemainTimeSeconds(counter.Timestamp, _configuration.ExpireTimeInSeconds);

            context.Response.Headers["X-Retry-After"] = counter.Timestamp.ToString();
            context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;

            await context.Response.WriteAsync($"Rate limit exceeded.Try again in #{remaingTime} seconds");
        }

        private async Task ProcessRequest(HttpContext context, string requestKey)
        {
            var counter = await _requestStore.GetAsync(requestKey);
            counter.Count++;

            await _requestStore.SetAsync(requestKey, counter);

            context.Response.Headers["X-Rate-Limit"] = _configuration.Limit.ToString();
            context.Response.Headers["X-Rate-Limit-Remaining"] = (_configuration.Limit - counter.Count).ToString();

            await _next(context);
        }
    }
}
