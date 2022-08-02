using Application.Common.Exceptions;
using Application.Modules.Commands.UpdateModule;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Modules.Commands
{
    public class UpdateModuleCommandTests
    {
        [Test]
        public async Task ShouldThrowNotFoundExceptionIfThereIsNoModule()
        {
            var command = new UpdateModuleCommand
            {
                ModuleId = 1,
                Name = "Animals",
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateModule()
        {
            var moduleId = await Testing.AddAsync(new Module
            {
                Name = "Animals",
                Note = "Animal names in english",
                Words = new List<Card>
                {
                    new Card { Term = "Кіт", Definition = "Cat"},
                    new Card { Term = "Собака", Definition = "Cow"}
                }
            });

            var command = new UpdateModuleCommand
            {
                ModuleId = moduleId,
                Name = "Animals - edited",
                Note = "Animal names in english",
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            await Testing.SendAsync(command);

            var module = await Testing.FindAsync<Module>(moduleId);

            module.Should().NotBeNull();
            module.Name.Should().Be(command.Name);
            module.Words[1].Definition.Should().Be(command.Words[1].Definition);
        }
    }
}
