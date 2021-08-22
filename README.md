# ApiRateLimiter by Anderson Fetter

This solution is made of 3 different projects.

* ApiRateLimiter.Tests Project
* ApiRateLimiter.Demo Project
* ApiRateLimiter Project

## ApiRateLimiter
My solution was based on S.O.L.I.D principles. I tried to demonstrate that I have the skills to write a high quality code, analyse and solve problems. As an example I didn't use Action Filter that is deeper in the request path and more expensive to process, instead I made the decision to use a Middleware. Middlewares are much lighter, something essential for a great scale solution. 

Another decision I made was to use Interface for the business logic, in order to have a better Unit test solution. Something very necessary in any professional application.

The decision to use Memory cache was a simple implementation but not escalable. For a escalable solution I would use an external high responsive DB like Redis.

I tried to keep the solution as clean as possible and easy to understand. 
Please reach out to me if you have any questions regarding the code.

## Tests
This solution contains a "SUT" project.

## Demo
This solution contains a Demo project to demonstrate its usability.

### Settings.
To change the settings it is necessary to change the file appsettings.json under Demo project.

```
  "RateLimitSettings": {
    "Limit": 5,
    "ExpireTimeInSeconds":  30
  }
```

###Integration
To use the Dll it is necessary to add the following lines on the Startup.cs file.

```
    services.Configure<RateLimitSettings>(Configuration.GetSection("RateLimitSettings"));
    services.AddMemoryCache();
    services.AddApiRateLimiter();
```
