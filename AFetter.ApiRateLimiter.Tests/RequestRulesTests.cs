using System;
using Xunit;

namespace AFetter.ApiRateLimiter.Tests
{
    public class RequestRulesTests
    {
        private readonly RequestRules _requestValidator;
        private static DateTime fixeFutureDateTime = DateTime.Now.AddHours(1);
        private static DateTime fixePastDateTime = DateTime.Now.AddHours(-1);

        public static readonly object[][] CorrectData_IsSessionExpired_Valid =
        {
                    new object[] { fixeFutureDateTime, 1 },
                    new object[] { fixeFutureDateTime, 60 },
                    new object[] { fixeFutureDateTime, 100 },
                    new object[] { fixeFutureDateTime, 60 * 60 },
        };

        public static readonly object[][] CorrectData_IsSessionExpired_Invalid =
        {
                    new object[] { fixePastDateTime, 1},
                    new object[] { fixePastDateTime, 20},
                    new object[] { fixePastDateTime, 30},
                    new object[] { fixePastDateTime, (60 * 60) + 1 },
        };

        public RequestRulesTests()
        {
            _requestValidator = new RequestRules();

        }

        [Theory]
        [InlineData(10, 20)]
        [InlineData(1, 1)]
        public void IsRequestCountValid_Count_Less_Than_Limit_Valid(int count, int limit)
        {
            var isRequestCountValid = _requestValidator.IsRequestCountValid(count, limit);
            Assert.True(isRequestCountValid);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(1, 0)]
        public void IsRequestCountValid_Count_Greater_Limit_Invalid(int count, int limit)
        {
            var isRequestCountValid = _requestValidator.IsRequestCountValid(count, limit);
            Assert.False(isRequestCountValid);
        }



        [Theory, MemberData(nameof(CorrectData_IsSessionExpired_Valid))]
        public void IsSessionExpired_Valid(DateTime dateTime, int expireTimeInSeconds)
        {
            var isSessionExpired = _requestValidator.IsSessionExpired(dateTime, expireTimeInSeconds);
            Assert.False(isSessionExpired);
        }

        [Theory, MemberData(nameof(CorrectData_IsSessionExpired_Invalid))]
        public void IsSessionExpired_Invalid(DateTime dateTime, int expireTimeInSeconds)
        {
            var isSessionExpired = _requestValidator.IsSessionExpired(dateTime, expireTimeInSeconds);
            Assert.True(isSessionExpired);
        }

    }
}
