using AFetter.ApiRateLimiter.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AFetter.ApiRateLimiter.Store
{
    public interface IMemoryCacheStore
    {
        Task<RateLimitData> GetAsync(string id, CancellationToken cancellationToken = default);
        Task SetAsync(string id, RateLimitData entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default);
    }
}
