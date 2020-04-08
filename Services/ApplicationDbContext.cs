using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickTrackWeb.Entities;
using System.Collections.Generic;

namespace QuickTrackWeb.Services
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ClassEntity> ClassEntities { get; set; }
        public DbSet<ProgressRecord> ProgressRecords { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TrackedItem> TrackedItems { get; set; }
        public DbSet<Week> Weeks { get; set; }

    }
}
