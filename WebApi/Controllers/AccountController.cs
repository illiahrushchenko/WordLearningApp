using Application.Common.Interfaces;
using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetJwtToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public AccountController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(GetJwtQuery getJwtQuery)
        {
            return Ok(await _mediator.Send(getJwtQuery));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
        {
            await _mediator.Send(createUserCommand);
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize(AuthenticationSchemes = "OAuth")]
        public IActionResult Test()
        {
            return Ok(new
            {
                Id = _currentUserService.UserId,
                Email = _currentUserService.UserEmail
            });
        }
    }
}
