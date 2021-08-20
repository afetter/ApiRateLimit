using System;

namespace AFetter.ApiRateLimiter.Model
{
    public class RateLimitData
    {
        public DateTime Timestamp { get; set; }
        public int Count { get; set; }
    }
}
