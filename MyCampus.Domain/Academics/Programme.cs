using MyCampus.Domain.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCampus.Domain.Academics
{
    public class Programme : FullAudit<int>
    {
        [StringLength(255)]
        public string NameEn { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<ProgrammeCatalog> ProgrammeCatalogs { get; set; }
    }
}
