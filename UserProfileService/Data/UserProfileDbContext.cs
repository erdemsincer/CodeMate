using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UserProfileService.Entities;

namespace UserProfileService.Data
{
    public class UserProfileDbContext : DbContext
    {
        public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserSkill> UserSkills => Set<UserSkill>();
    }
}
