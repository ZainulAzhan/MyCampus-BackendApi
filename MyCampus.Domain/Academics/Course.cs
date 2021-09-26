using MyCampus.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace MyCampus.Domain.Academics
{
    public class Course : FullAudit<int>
    {
        [StringLength(255)]
        public string NameEn { get; set; }

        [StringLength(10)]
        public string Code { get; set; }
    }
}
