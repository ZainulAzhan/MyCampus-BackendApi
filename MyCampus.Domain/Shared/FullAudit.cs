using System;
using System.ComponentModel.DataAnnotations;

namespace MyCampus.Domain.Shared
{
    public abstract class FullAudit<T> : IEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedOn { get; set; }

        [StringLength(20)]
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [StringLength(20)]
        public string ModifiedBy { get; set; }
    }
}
