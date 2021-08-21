using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AFetter.ApiRateLimiter.Tests
{
    public class SingleAccessTest : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public SingleAccessTest(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Privacy")]
        public async Task AnonymousAccess_Single_Access(string url)
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}?1");
            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
