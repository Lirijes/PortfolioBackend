using Microsoft.EntityFrameworkCore;
using portfolioApi.Models;

namespace portfolioApi.Context
{
    public class ProfileContext : DbContext
    {
        public ProfileContext(DbContextOptions<ProfileContext> options)
        : base(options)
        { 
        }

        public DbSet<ProfileInfo> Profiles { get; set; }
        public DbSet<ProfileEducation> ProfileEducations { get; set; }
        public DbSet<ProfileExperience> ProfileExperiences { get; set; }
        public DbSet<ProfileLink> ProfileLinks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Utility> Utilities { get; set; }
        public DbSet<ProjectUtility> ProjectUtilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // many-to-many-realtionship between project and utilites
            modelBuilder.Entity<ProjectUtility>()
                .HasKey(pu => new { pu.ProjectId, pu.UtilityId }); // define PK

            modelBuilder.Entity<ProjectUtility>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.ProjectUtilities)
                .HasForeignKey(pu => pu.ProjectId);

            modelBuilder.Entity<ProjectUtility>()
                .HasOne(pu => pu.Utility)
                .WithMany(u => u.ProjectUtilities)
                .HasForeignKey(pu => pu.UtilityId);
        }
    }
}
