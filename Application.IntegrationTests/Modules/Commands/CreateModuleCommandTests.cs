using Application.Common.Exceptions;
using Application.Modules.Commands.CreateModule;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Modules.Commands
{
    public class CreateModuleCommandTests
    {
        [Test]
        public async Task ShouldCreateModule()
        {
            var email = "andrew@mail.com";

            var userId = await Testing.CreateUser(email, "Andrew", "1234");

            var command = new CreateModuleCommand
            {
                Name = "Animals",
                OwnerEmail = email,
                Words = new List<CreateCardCommand>
                {
                    new CreateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new CreateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            var moduleId = await Testing.SendAsync(command);

            var module = await Testing.FindAsync<Module>(moduleId);

            module.Should().NotBeNull();
            module.OwnerId.Should().Be(userId);
            module.Name.Should().Be(command.Name);
        }

        [Test]
        public async Task ShouldThrowNotFoundExceptionIfThereIsNoUser()
        {
            var command = new CreateModuleCommand
            {
                Name = "Animals",
                OwnerEmail = "andrew@mail.com",
                Words = new List<CreateCardCommand>
                {
                    new CreateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new CreateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }
    }
}
