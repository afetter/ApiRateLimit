using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AFetter.ApiRateLimiter.Tests
{
    public class AbusiveAccessInsideLimitsTest : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public AbusiveAccessInsideLimitsTest(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Theory]
        [InlineData("/", 5)]
        [InlineData("/Privacy", 5)]
        public async Task AnonymousAccess_Abusive_Access_Inside_Limits(string url, int times)
        {
            // Arrange

            for (int i = 1; i <= times; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{url}?2");
                var response = await _client.SendAsync(request);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }

        }

    }
}
