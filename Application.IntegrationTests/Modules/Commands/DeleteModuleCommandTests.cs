using Application.Common.Exceptions;
using Application.Modules.Commands.CreateModule;
using Application.Modules.Commands.DeleteModule;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Modules.Commands
{
    class DeleteModuleCommandTests
    {
        [Test]
        public async Task ShouldDeleteModule()
        {
            var email = "andrew@mail.com";

            var userId = await Testing.CreateUserAsync(email, "Andrew", "1234");

            var moduleId = await Testing.SendAsync(new CreateModuleCommand
            {
                Name = "Animals",
                IsPublic = true,
                Words = new List<CreateCardCommand>
                {
                    new CreateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new CreateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            });

            var command = new DeleteModuleCommand
            {
                Id = moduleId
            };

            await Testing.SendAsync(command);

            var module = await Testing.FindAsync<Module>(moduleId);

            module.Should().BeNull();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowNotFoundExceptionWhenNoModule()
        {
            var email = "andrew@mail.com";

            await Testing.CreateUserAsync(email, "Andrew", "1234");

            var command = new DeleteModuleCommand
            {
                Id = 1
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenNoId()
        {
            var email = "andrew@mail.com";

            await Testing.CreateUserAsync(email, "Andrew", "1234");

            var command = new DeleteModuleCommand();

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }
    }
}
