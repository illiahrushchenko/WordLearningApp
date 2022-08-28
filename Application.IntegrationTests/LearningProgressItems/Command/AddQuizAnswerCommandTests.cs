using Application.Common.Exceptions;
using Application.LearningProgresses.Commands.CreateLearningProgress;
using Application.LearningProgressItems.Commands.AddQuizAnswer;
using Application.LearningProgressItems.Queries.GetNextQuizQuestion;
using Application.Modules.Commands.CreateModule;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.LearningProgressItems.Command
{
    public class AddQuizAnswerCommandTests : TestBase
    {
        [Test]
        public async Task ShouldThrowValidationExceptionWhenLearningProgressItemIdIsZero()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new AddQuizAnswerCommand
            {
                LearningProgressItemId = 0
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}
