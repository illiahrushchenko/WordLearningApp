using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests
{
    using global::Application.Common.Interfaces;
    using Infrastructure.Persistence;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using System;
    using WebApi;

    namespace Application.IntegrationTests
    {
        class CustomWebApplicationFactory : WebApplicationFactory<Startup>
        {
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                base.ConfigureWebHost(builder);
                builder.ConfigureServices(services =>
                {
                    var currentuserserviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ICurrentUserService));
                    services.Remove(currentuserserviceDescriptor);
                    services.AddTransient(provider => Mock.Of<ICurrentUserService>(s => 
                        s.UserId == Testing.GetCurrentUserData().Id && 
                        s.UserEmail == Testing.GetCurrentUserData().Email));

                    var contextserviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                    services.Remove(contextserviceDescriptor);
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });
                });
            }
        }
    }
}
