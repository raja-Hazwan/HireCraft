// Data/ApplicationDbContext.cs
using HireCraft.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace HireCraft.Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Resume> Resumes => Set<Resume>();
        public DbSet<Education> Educations => Set<Education>();
        public DbSet<Experience> Experiences => Set<Experience>();
        public DbSet<Skill> Skills => Set<Skill>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Fluent configurations (optional)...
            builder.Entity<Resume>()
                   .HasMany(r => r.Educations)
                   .WithOne(e => e.Resume)
                   .HasForeignKey(e => e.ResumeId);

            builder.Entity<Resume>()
                   .HasMany(r => r.Experiences)
                   .WithOne(x => x.Resume)
                   .HasForeignKey(x => x.ResumeId);

            builder.Entity<Resume>()
                   .HasMany(r => r.Skills)
                   .WithOne(s => s.Resume)
                   .HasForeignKey(s => s.ResumeId);
        }
    }
}
