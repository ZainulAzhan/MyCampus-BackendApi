using System;

namespace MyCampus.Domain.Shared
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

    }
}
