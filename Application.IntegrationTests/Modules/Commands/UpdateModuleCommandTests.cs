﻿using Application.Common.Exceptions;
using Application.Modules.Commands.CreateModule;
using Application.Modules.Commands.UpdateModule;
using Application.Modules.Queries.GetModuleDetails;
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
        public async Task ShouldThrowNotFoundExceptionWhenNoModule()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new UpdateModuleCommand
            {
                Id = 1,
                Name = "Animals",
                IsPublic = true,
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldUpdateModule()
        {
            var email = "andrew@mail.com";

            await Testing.CreateUserAsync(email, "Andrew", "1234");

            var moduleId = await Testing.SendAsync(new CreateModuleCommand
            {
                Name = "Animals",
                IsPublic = true,
                Words = new List<CreateCardCommand>
                {
                    new CreateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new CreateCardCommand{ Term = "Собака", Definition = "Cow"},
                }
            });

            var command = new UpdateModuleCommand
            {
                Id = moduleId,
                Name = "Animals - edited",
                Note = "Animal names in english",
                IsPublic = true,
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            await Testing.SendAsync(command);

            var module = await Testing.SendAsync(new GetModuleDetailsQuery 
            {
                Id = moduleId
            });

            module.Should().NotBeNull();
            module.Name.Should().Be(command.Name);
            module.Words[1].Definition.Should().Be(command.Words[1].Definition);

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenNoName()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new UpdateModuleCommand
            {
                Id = 1,
                IsPublic = true,
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenNoId()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new UpdateModuleCommand
            {
                Name = "Animals",
                IsPublic = true,
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenNoIsPublicProp()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new UpdateModuleCommand
            {
                Name = "Animals",
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Собака", Definition = "Dog"},
                }
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenShortList()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new UpdateModuleCommand
            {
                Name = "Animals",
                IsPublic = true,
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                }
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }

        [Test]
        public async Task ShouldThrowValidationExceptionWhenLongList()
        {
            await Testing.CreateUserAsync("andrew@mail.com", "Andrew", "1234");

            var command = new UpdateModuleCommand
            {
                Name = "Animals",
                IsPublic = true,
                Words = new List<UpdateCardCommand>
                {
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                    new UpdateCardCommand{ Term = "Кіт", Definition = "Cat"},
                }
            };

            await FluentActions.Invoking(() =>
                Testing.SendAsync(command)).Should().ThrowAsync<ValidationException>();

            await Testing.ResetAsync();
        }
    }
}