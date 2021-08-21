using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AFetter.ApiRateLimiter.Tests
{
    public class AbusiveAccessInsideWaitNewWindowTest : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public AbusiveAccessInsideWaitNewWindowTest(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Theory]
        [InlineData("/", 5)]
        [InlineData("/Privacy", 5)]
        public async Task AnonymousAccess_Abusive_Access_Outside_Limits_Wait_New_Window(string url, int times)
        {
            // Arrange

            for (int i = 1; i <= times; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

            await Task.Delay(1010);

            var requestAbusive = new HttpRequestMessage(HttpMethod.Get, url);
            var responseAbusive = await _client.SendAsync(requestAbusive);
            Assert.Equal(HttpStatusCode.OK, responseAbusive.StatusCode);

        }

    }
}
