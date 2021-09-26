using MyCampus.Domain.Shared;

namespace MyCampus.Domain.Academics
{
    public class ProgrammeCatalog : FullAudit<int>
    {
        public int ProgrammeId { get; set; }
        public virtual Programme Programme { get; set; }
        public int CourseCatalogId { get; set; }
        public virtual CourseCatalog CourseCatalog { get; set; }
    }
}
