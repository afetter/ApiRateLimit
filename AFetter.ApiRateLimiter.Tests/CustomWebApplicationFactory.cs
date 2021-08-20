using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFetter.ApiRateLimiter.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Demo.Startup>
    {
    }
}
