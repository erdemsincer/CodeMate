using MassTransit;
using MatchingService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MatchingService.Data
{
    public class MatchingDbContext : DbContext
    {
        public MatchingDbContext(DbContextOptions<MatchingDbContext> options) : base(options) { }

        public DbSet<Matching> Matchings { get; set; }
    }

}
