using Game.Models;
using Microsoft.EntityFrameworkCore;

namespace Game.Context
{
    public class EFContext : DbContext
    {
        public DbSet<Supporter> Supporters { get; set; }
        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.//DB//heroes.db");
        }
    }
}
