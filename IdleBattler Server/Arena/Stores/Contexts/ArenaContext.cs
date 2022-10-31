using IdleBattler_Common.Models.Arena;
using Microsoft.EntityFrameworkCore;

namespace IdleBattler_Server.Arena.Stores.Contexts
{
    public class ArenaContext : DbContext
    {
        public ArenaContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        //entities
        public DbSet<ArenaModel> Arenas { get; set; }
    }
}
