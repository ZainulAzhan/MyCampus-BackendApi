using AutoMapper;
using MediatR;
using MyCampus.Data.Context;
using MyCampus.Domain.PersonRoles;
using MyCampus.Service.Dtos.PersonRoles;
using MyCampus.Service.Helpers;
using System;
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
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(CampusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegisterOutputDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            byte[] salt = AccountHelper.GenerateSalt();
            DateTime datetime = DateTime.UtcNow;
            AppUser appUser = new ()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Username,
                Password = AccountHelper.GenerateHashedPassword(request.Password, salt),
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
    }
}
