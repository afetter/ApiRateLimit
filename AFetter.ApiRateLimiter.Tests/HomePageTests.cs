using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AFetter.ApiRateLimiter.Tests
{
    public class HomePageTests : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;

        public HomePageTests(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [NUnit.Framework.Theory]
        [InlineData("/")]
        public async Task AnonymousAccess(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _client.SendAsync(request);

            Assert.Equals(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
