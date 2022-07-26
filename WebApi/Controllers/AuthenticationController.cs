using Application.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(CreateUserCommand createUserCommand)
        {
            await _mediator.Send(createUserCommand);
            return Ok();
        }
    }
}
