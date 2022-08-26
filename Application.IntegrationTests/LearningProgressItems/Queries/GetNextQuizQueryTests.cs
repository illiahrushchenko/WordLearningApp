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

namespace Application.IntegrationTests.LearningProgressItems.Queries
{
    public class GetNextQuizQueryTests
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

            await Testing.SendAsync(new CreateLearningProgressCommand
            {
                ModuleId = moduleId
            });

            var quizDto = await Testing.SendAsync(new GetNextQuizQuery
            {
                ModuleId = moduleId
            });

            quizDto.Should().NotBeNull();
            quizDto.Term.Should().Be(command.Words[0].Term);
            quizDto.Options.Should().OnlyHaveUniqueItems()
                .And.HaveCount(2)
                .And.Contain(command.Words[0].Definition);

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenModuleIdIsZero()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new GetNextQuizQuery
            {
                ModuleId = 0
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowNotFoundExceptionWhenNoModule()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new GetNextQuizQuery
            {
                ModuleId = 1
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowNotFoundExceptionWhenNoLearningProgress()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            await Testing.SendAsync(new CreateModuleCommand
            {
                Name = "Animals",
                IsPublic = true,
                Words = new List<CreateCardCommand>
                {
                    new CreateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new CreateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            });

            var command = new GetNextQuizQuery
            {
                ModuleId = 1
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();

            await Testing.ResetAsync();
        }

        public async Task ShouldThrowNotFoundExceptionWhenAllQuizesAnswered()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            //Initialization
            var createModuleCommand = new CreateModuleCommand
            {
                Name = "Animals",
                IsPublic = true,
                Words = new List<CreateCardCommand>
                {
                    new CreateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new CreateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            var moduleId = await Testing.SendAsync(createModuleCommand);

            await Testing.SendAsync(new CreateLearningProgressCommand
            {
                ModuleId = moduleId
            });

            //Answering all quizes
            for (int i = 0; i < 2; i++)
            {
                var nextQuiz = await Testing.SendAsync(new GetNextQuizQuery
                {
                    ModuleId = moduleId
                });

                await Testing.SendAsync(new AddQuizAnswerCommand
                {
                    CardId = nextQuiz.CardId,
                    Answer = createModuleCommand.Words[i].Definition
                });
            }

            //Get next quiz when all quizes alredy answered
            var command = new GetNextQuizQuery
            {
                ModuleId = moduleId
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();

            await Testing.ResetAsync();
        }
    }
}
