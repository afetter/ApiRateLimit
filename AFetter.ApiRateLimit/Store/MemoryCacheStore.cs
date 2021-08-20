﻿using AFetter.ApiRateLimiter.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AFetter.ApiRateLimiter.Store
{
    public class MemoryCacheStore: IMemoryCacheStore
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheStore(IMemoryCache cache)
        {
            _cache = cache;
        }

        public Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_cache.TryGetValue(id, out _));
        }

        public Task<RateLimitData> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            if (_cache.TryGetValue(id, out RateLimitData stored))
            {
                return Task.FromResult(stored);
            }

            return Task.FromResult(new RateLimitData
            {
                Count = 0,
                Timestamp = DateTime.UtcNow
            }) ;
        }

        public Task SetAsync(string id, RateLimitData entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
        {
            var options = new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            };

            if (expirationTime.HasValue)
            {
                options.SetAbsoluteExpiration(expirationTime.Value);
            }

            _cache.Set(id, entry, options);

            return Task.CompletedTask;
        }
    }
}