using Application.Common.Exceptions;
using Application.LearningProgresses.Commands.CreateLearningProgress;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.LearningProgresses.Commands
{
    public class CreateLearningProgressCommandTests : TestBase
    {
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
        }
    }
}
