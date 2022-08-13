using Application.IntegrationTests.Application.IntegrationTests;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using Respawn.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi;


namespace Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static WebApplicationFactory<Startup> _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;
        private static Checkpoint _checkpoint = null!;

        private static UserData _currentUserData;

        public static UserData GetCurrentUserData()
        {
            return _currentUserData;
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new Table[] { new Table("__EFMigrationsHistory") }
            };
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task<int> CreateUserAsync(string email, string userName, string password)
        {
            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();


            var user = new User
            {
                Email = email,
                UserName = userName
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                _currentUserData = new UserData
                {
                    Id = user.Id,
                    Email = user.Email
                };

                return _currentUserData.Id;
            }
            else if (await userManager.Users.AnyAsync(x => x.Email == email))
            {
                return _currentUserData.Id;
            }

            var errors = string.Join(Environment.NewLine, result.Errors.Select(x => x.Description));

            throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
        }

        public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task<int> AddAsync<TEntity>(TEntity entity)
            where TEntity : EntityBase
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();

            return entity.Id;
        }

        public static async Task ResetAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.EnsureDeletedAsync();
        }
    }
}
