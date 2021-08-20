﻿using System;

namespace AFetter.ApiRateLimiter
{
    public interface IRequestValidator
    {
        bool IsRequestCountValid(int count, int limit);
        bool IsSessionExpired(DateTime datetime, int expireTimeInSeconds);
        int GetRemainTimeSeconds(DateTime datetime, int expireTimeInSeconds);
    }
}
