
using System;

namespace MyCampus.Service.Dtos.PersonRoles
{
    public class RegisterOutputDto
    {
        public Guid Id { get; set; }
        public int TenantId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

    }
}
