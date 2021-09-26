using MyCampus.Domain.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCampus.Domain.Academics
{
    public class Faculty : FullAudit<int>
    {
        [StringLength(255)]
        public string NameEn { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
        public virtual ICollection<Department> Departments { get; set; }

    }
}
