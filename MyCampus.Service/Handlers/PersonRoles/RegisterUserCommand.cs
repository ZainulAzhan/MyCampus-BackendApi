using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MyCampus.Data.Context;
using MyCampus.Domain.PersonRoles;
using MyCampus.Service.Dtos.PersonRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyCampus.Service.Handlers.PersonRoles
{

    public class RegisterUserCommand : IRequest<RegisterOutputDto>
    {
        public int TenantId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterOutputDto>
    {
        private readonly CampusDbContext _context;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(CampusDbContext context, IMediator mediator, IMapper mapper)
        {
            _context = context;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<RegisterOutputDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            byte[] salt = GenerateSalt();
            DateTime datetime = DateTime.UtcNow;
            AppUser appUser = new ()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Username,
                Password = GenerateHashedPassword(request.Password, salt),
                Salt = salt,
                CreatedOn = datetime,
                ModifiedOn = datetime,
                TenantId = request.TenantId
            };
            _context.AppUsers.Add(appUser);
            await _context.SaveChangesAsync();
            var output = _mapper.Map<AppUser, RegisterOutputDto>(appUser);
            output.Token = "This is your token";
            return output;
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }

        private byte[] GenerateHashedPassword(string password, byte[] salt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 10,000 iterations)
            byte[] hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);
            return hashed;
        }
    }
}
