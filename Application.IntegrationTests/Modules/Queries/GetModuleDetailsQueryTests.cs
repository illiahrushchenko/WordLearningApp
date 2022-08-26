using Application.Common.Exceptions;
using Application.Modules.Commands.CreateModule;
using Application.Modules.Queries.GetModuleDetails;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Modules.Queries
{
    public class GetModuleDetailsQueryTests : TestBase
    {
        [Test]
        public async Task ShouldGetModule()
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

            var module = await Testing.SendAsync(new GetModuleDetailsQuery
            {
                Id = moduleId
            });

            module.Should().NotBeNull();
            module.Words.Should().HaveCount(2);
            module.Name.Should().Be(command.Name);
            module.IsPublic.Should().Be(command.IsPublic);
            module.Note.Should().Be(command.Note);
        }

        [Test]
        public async Task ShouldThrowNotFoundExceptionWhenNoModule()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new GetModuleDetailsQuery
            {
                Id = 1
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenNoId()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new GetModuleDetailsQuery();

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}
