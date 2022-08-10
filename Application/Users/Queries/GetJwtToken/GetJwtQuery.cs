using Application.Common.Exceptions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetJwtToken
{
    public class GetJwtQuery : IRequest<AuthenticationDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    class GetJwtTokenQueryHandler : IRequestHandler<GetJwtQuery, AuthenticationDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public GetJwtTokenQueryHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthenticationDto> Handle(GetJwtQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user == null)
            {
                throw new UnauthorizedException($"User with email \"{request.Email}\" was not found");
            }
            else if(!await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new UnauthorizedException($"Invalid password");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Iss"],
                _configuration["Jwt:Aud"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(7),
                signingCredentials);

            return new AuthenticationDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
