using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UltraBet.Data.Configurations;
using UltraBet.Data.Models;

namespace UltraBet.Data
{
    public class BetPlusDbContext : IdentityDbContext<ApplicationUser>
    {
        public BetPlusDbContext(DbContextOptions<BetPlusDbContext> options)
            : base(options)
        {

        }
        public DbSet<Sport> Sports { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Bet> Bets { get; set; }

        public DbSet<Odd> Odds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SportConfig());

            builder.ApplyConfiguration(new EventConfig());

            builder.ApplyConfiguration(new MatchConfig());

            builder.ApplyConfiguration(new BetConfig());

            builder.ApplyConfiguration(new OddConfig());

            base.OnModelCreating(builder);
        }
    }
}
