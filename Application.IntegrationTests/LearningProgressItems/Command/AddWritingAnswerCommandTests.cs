using Application.Common.Exceptions;
using Application.LearningProgressItems.Commands.AddWritingAnswer;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.LearningProgressItems.Command
{
    public class AddWritingAnswerCommandTests : TestBase
    {
        [Test]
        public async Task ShouldThrowValidationExceptionWhenLearningProgressItemIdIsZero()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new AddWritingAnswerCommand
            {
                LearningProgressItemId = 0
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}
