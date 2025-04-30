using Microsoft.EntityFrameworkCore;
using OfferService.Entities;
using System.Collections.Generic;

namespace OfferService.Data
{
    public class OfferDbContext : DbContext
    {
        public OfferDbContext(DbContextOptions<OfferDbContext> options) : base(options) { }

        public DbSet<Offer> Offers { get; set; }
    }
}
