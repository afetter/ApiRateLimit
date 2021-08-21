using AFetter.ApiRateLimiter;
using AFetter.ApiRateLimiter.Store;
using Microsoft.Extensions.DependencyInjection;

namespace AFetter.ApiRateLimit.Extension
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddApiRateLimiter(this IServiceCollection services)
        {
            services.AddSingleton<IMemoryCacheStore, MemoryCacheStore>();
            services.AddTransient<IRequestRules, RequestRules>();
            return services;
        }
    }
}
