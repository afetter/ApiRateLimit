# ApiRateLimiter by Anderson Fetter

This solution is composed by 3 different projects.

*ApiRateLimiter.Tests Project
*ApiRateLimiter.Demo Project
*ApiRateLimiter Project

## Tests
This solution contains a "SUT" project.

## Demo
This solution contains a Demo project where all the requirements.

### Settings.
To change the settings it is necessary to change the file appsettings.json under Demo project.

```
  "RateLimitSettings": {
    "Limit": 5,
    "ExpireTimeInSeconds":  30
  }
```

###Integration
To use the Dll it's necessary to add the following lines on the Startup.cs file.

```
    services.Configure<RateLimitSettings>(Configuration.GetSection("RateLimitSettings"));
    services.AddMemoryCache();
    services.AddApiRateLimiter();
```

## ApiRateLimiter
This is the project with Interfaces to facilitate Unit Tests, Business Logic and a Middleware to intercept the request and verify if the User and the patch are inside of the rules of the appsettings.json