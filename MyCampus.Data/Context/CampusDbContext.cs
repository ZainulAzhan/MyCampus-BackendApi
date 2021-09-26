using Microsoft.EntityFrameworkCore;
using MyCampus.Domain.Academics;
using MyCampus.Domain.PersonRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCampus.Data.Context
{
    public class CampusDbContext : DbContext
    {
        public CampusDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCatalog> CourseCatalogs { get; set; }
        public DbSet<CourseCatalogItem> CourseCatalogItems { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Programme> Programmes { get; set; }
        public DbSet<ProgrammeCatalog> ProgrammeCatalogs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

    }
}
