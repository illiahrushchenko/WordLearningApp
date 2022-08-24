using Application.Common.Exceptions;
using Application.Modules.Commands.CreateModule;
using Application.Quizes.Queries.GetQuiz;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IntegrationTests.Quizes.Queries
{
    public class GetQuizQueryTests
    {
        [Test]
        public async Task ShouldGetQuiz()
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

            var quizDto = await Testing.SendAsync(new GetQuizQuery 
            {
                ModuleId = moduleId,
                CardNumber = 0
            });

            quizDto.Should().NotBeNull();
            quizDto.Term.Should().Be(command.Words[0].Term);
            quizDto.Options.Should().OnlyHaveUniqueItems()
                .And.HaveCountLessThanOrEqualTo(4)
                .And.Contain(command.Words[0].Definition);

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowNotFoundExceptionWhenNoModule()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new GetQuizQuery
            {
                ModuleId = 1,
                CardNumber = 0
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowNotFoundExceptionWhenFewCards()
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

            var command = new GetQuizQuery
            {
                ModuleId = moduleId,
                CardNumber = 5
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenModuleIdIsZero()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new GetQuizQuery
            {
                ModuleId = 0,
                CardNumber = 5
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }
    }
}
