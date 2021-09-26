using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyCampus.Data.Context;
using MyCampus.Domain.PersonRoles;
using MyCampus.Service.Dtos.Account;
using MyCampus.Service.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCampus.Service.Handlers.Accounts
{
    public class LoginCommand : IRequest<LoginOutputDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int TenantId { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginOutputDto>
    {
        private readonly CampusDbContext _context;
        private readonly IConfiguration _config;

        public LoginCommandHandler(CampusDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<LoginOutputDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _context.AppUsers.Where(c => c.UserName.Equals(request.Username) &&
                c.TenantId.Equals(request.TenantId)).FirstOrDefaultAsync();
            if(appUser == null)
            {
                throw new Exception("Username or password not valid");
            }
            if (CheckPassword(appUser, request.Password))
            {
                return GenerateToken(appUser);
            }
            throw new Exception("Username or password not valid");
        }

        private bool CheckPassword(AppUser appUser, string password)
        {
            var hashedPassword = AccountHelper.GenerateHashedPassword(password, appUser.Salt);
            for(int i = 0; i < appUser.Password.Length; i++)
            {
                if(appUser.Password[i] != hashedPassword[i])
                {
                    return false;
                }
            }
            return true;
        }

        private LoginOutputDto GenerateToken(AppUser appUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, appUser.TenantId.ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            LoginOutputDto output = new ();
            output.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return output;
        }
    }
}
