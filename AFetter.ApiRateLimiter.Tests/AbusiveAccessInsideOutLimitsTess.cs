using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AFetter.ApiRateLimiter.Tests
{
    public class AbusiveAccessInsideOutLimitsTess : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public AbusiveAccessInsideOutLimitsTess(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Theory]
        [InlineData("/", 5)]
        [InlineData("/Privacy", 5)]
        public async Task AnonymousAccess_Abusive_Access_Outside_Limits(string url, int times)
        {
            // Arrange

            for (int i = 1; i <= times; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            var requestAbusive = new HttpRequestMessage(HttpMethod.Get, url);
            var responseAbusive = await _client.SendAsync(requestAbusive);
            Assert.Equal(HttpStatusCode.TooManyRequests, responseAbusive.StatusCode);

        }

    }
}
