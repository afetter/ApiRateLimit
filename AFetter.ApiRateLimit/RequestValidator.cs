using System;

namespace AFetter.ApiRateLimiter
{

    public class RequestValidator : IRequestValidator
    {
        public int GetRemainTimeSeconds(DateTime datetime, int expireTimeInSeconds)
        {
            return (int)(expireTimeInSeconds - DateTime.UtcNow.Subtract(datetime).TotalSeconds);
        }

        public bool IsRequestCountValid(int count, int limit) 
            => count < limit;

        public bool IsSessionExpired(DateTime datetime, int expireTimeInSeconds) 
            => datetime.AddSeconds(expireTimeInSeconds) < DateTime.UtcNow;
    }
}
