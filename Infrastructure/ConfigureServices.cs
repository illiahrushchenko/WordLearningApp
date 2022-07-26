using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                        IConfiguration configuration)
        {
            string connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(connection);
            })
            .AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
