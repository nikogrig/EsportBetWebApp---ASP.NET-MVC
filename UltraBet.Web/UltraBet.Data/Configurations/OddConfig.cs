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
    public class OddConfig : IEntityTypeConfiguration<Odd>
    {
        public void Configure(EntityTypeBuilder<Odd> entity)
        {
            entity.HasKey(o => o.Id);

            entity.HasOne(o => o.Bet)
                .WithMany(b => b.Odds)
                .HasForeignKey(o => o.BetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}