using ChatService.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChatService.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
