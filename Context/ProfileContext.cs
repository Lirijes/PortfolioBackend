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
        public DbSet<PhoneNumberRequest> PhoneNumberRequest { get; set; }
    }
}
