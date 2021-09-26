using MyCampus.Domain.Shared;

namespace MyCampus.Domain.Academics
{
    public class CourseCatalogItem : FullAudit<int>
    {
        public int Term { get; set; }
        public int CourseId { get; set; }
        public int CourseCatalogId { get; set; }
        public virtual Course Course { get; set; }
        public virtual CourseCatalog CourseCatalog { get; set; }
    }
}
