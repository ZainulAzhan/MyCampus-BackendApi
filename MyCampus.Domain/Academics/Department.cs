using MyCampus.Domain.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCampus.Domain.Academics
{
    public class Department : FullAudit<int>
    {
        [StringLength(255)]
        public string NameEn { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
        public int FacultyId { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<Programme> Programmes { get; set; }
    }
}
