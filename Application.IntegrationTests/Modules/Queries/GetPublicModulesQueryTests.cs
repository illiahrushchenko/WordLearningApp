using Application.Common.Exceptions;
using Application.Modules.Commands.CreateModule;
using Application.Modules.Queries.GetModuleDetails;
using Application.Modules.Queries.GetPublicModules;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Modules.Queries
{
    public class GetPublicModulesQueryTests : TestBase
    {
        [Test]
        public async Task ShouldGetModules()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new CreateModuleCommand
            {
                Name = "Animals",
                IsPublic = true,
                Words = new List<CreateCardCommand>
                {
                    new CreateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new CreateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            var moduleId = await Testing.SendAsync(command);

            var publicModules = await Testing.SendAsync(new GetPublicModulesQuery
            {
                PageNumber = 1,
                PageSize = 2
            });

            publicModules.Items.Should().NotBeEmpty()
                .And.ContainSingle(x => x.Name == command.Name)
                .And.HaveCountGreaterThanOrEqualTo(1);
            publicModules.HasNextPage.Should().BeFalse();
            publicModules.HasPreviousPage.Should().BeFalse();
        }

        [Test]
        public async Task ShouldGetEmptyListWhenNoPublicModules()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new CreateModuleCommand
            {
                Name = "Animals",
                IsPublic = false,
                Words = new List<CreateCardCommand>
                {
                    new CreateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new CreateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            var moduleId = await Testing.SendAsync(command);

            var module = await Testing.SendAsync(new GetModuleDetailsQuery
            {
                Id = moduleId
            });

            var publicModules = await Testing.SendAsync(new GetPublicModulesQuery
            {
                PageNumber = 1,
                PageSize = 2
            });

            publicModules.Items.Should().BeEmpty();
            publicModules.HasNextPage.Should().BeFalse();
            publicModules.HasPreviousPage.Should().BeFalse();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenPageNumberIsZero()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new GetPublicModulesQuery
            {
                PageNumber = 0,
                PageSize = 2
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenPageSizeIsZero()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new GetPublicModulesQuery
            {
                PageNumber = 1,
                PageSize = 0
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}
