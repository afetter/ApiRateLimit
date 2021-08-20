namespace AFetter.ApiRateLimiter.Model
{
    public class RateLimitSettings
    {
        public int Limit { get; set; }
        public int ExpireTimeInSeconds { get; set; }
    }
}
