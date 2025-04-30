using CourseService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CourseService.Data
{
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
    }
}
