using Application.Common.Exceptions;
using Application.Modules.Commands.CreateModule;
using Application.Quizes.Commands.CreateLearningProgress;
using Application.Quizes.Queries.GetLearningProgress;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizes.Commands
{
    public class CreateLearningProgressCommandTests
    {
        [Test]
        public async Task ShouldGetProgress()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

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

            await Testing.SendAsync(new CreateLearningProgressCommand
            {
                ModuleId = moduleId
            });

            var learningProgress = await Testing.SendAsync(new GetLearningProgressQuery
            {
                ModuleId = moduleId
            });

            learningProgress.LearningProgressItems.Should().HaveCount(2);

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowNotFoundExceptionWhenNoModule()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new CreateLearningProgressCommand
            {
                ModuleId = 1
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenModuleIdIsZero()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new CreateLearningProgressCommand
            {
                ModuleId = 0
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }
    }
}
