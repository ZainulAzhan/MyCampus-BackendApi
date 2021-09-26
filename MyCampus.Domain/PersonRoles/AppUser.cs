using MyCampus.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCampus.Domain.PersonRoles
{
    public class AppUser : FullAudit<Guid>
    {
        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [MaxLength(256/8)]
        public byte[] Password { get; set; }

        [MaxLength(128 / 8)]
        public byte[] Salt { get; set; }
    }
}
