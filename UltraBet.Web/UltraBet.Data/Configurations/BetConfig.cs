using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraBet.Data.Models;

namespace UltraBet.Data.Configurations
{
    public class BetConfig : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> entity)
        {
            entity.HasKey(b => b.Id);

            entity.HasOne(b => b.Match)
                .WithMany(m => m.Bets)
                .HasForeignKey(b => b.MatchId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}