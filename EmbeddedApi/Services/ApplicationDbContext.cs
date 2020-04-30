using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickTrackWeb.EmbeddedApi.Entities;

namespace QuickTrackWeb.EmbeddedApi.Services
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<ClassEntity> ClassEntities { get; set; }
        public DbSet<ProgressRecord> ProgressRecords { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<TrackedItem> TrackedItems { get; set; }
        public DbSet<Week> Weeks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ClassEntity>()
                .HasMany(a => a.ProgressRecords)
                .WithOne(b => b.ClassEntity)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>()
                .HasMany(a => a.ProgressRecords)
                .WithOne(b => b.Student)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TrackedItem>()
                .HasMany(a => a.ProgressRecords)
                .WithOne(b => b.TrackedItem)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Week>()
                .HasMany(a => a.ProgressRecords)
                .WithOne(b => b.Week)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClassEntity>()
                .HasMany(a => a.Students)
                .WithOne(b => b.ClassEntity)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClassEntity>()
                .HasMany(a => a.TrackedItems)
                .WithOne(b => b.ClassEntity)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ClassEntity>()
                .HasMany(a => a.Weeks)
                .WithOne(b => b.ClassEntity)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            //BELOW MIGHT WORK! ---> https://entityframeworkcore.com/knowledge-base/34768976/specifying-on-delete-no-action-in-entity-framework-7-
            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}

            base.OnModelCreating(modelBuilder);
        }

    }
}
