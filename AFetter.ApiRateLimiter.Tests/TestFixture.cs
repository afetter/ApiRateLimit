using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace AFetter.ApiRateLimiter.Tests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;

        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Demo.Startup>()
                .ConfigureAppConfiguration((context, config) =>
                {
                    //config.SetBasePath(Path.Combine(
                    //    Directory.GetCurrentDirectory(),
                    //    "..\\..\\..\\..\\MyOffer.Web"));

                    //config.AddJsonFile("appsettings.json");
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:8888");
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
