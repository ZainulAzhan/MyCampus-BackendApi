using MyCampus.Domain.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyCampus.Domain.Academics
{
    public class CourseCatalog : FullAudit<int>
    {
        [StringLength(10)]
        public string Version { get; set; }
        public virtual ICollection<CourseCatalogItem> CourseCatalogItems { get; set; }

    }
}
