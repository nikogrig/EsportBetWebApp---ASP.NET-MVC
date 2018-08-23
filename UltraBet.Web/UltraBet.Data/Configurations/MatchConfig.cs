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
    public class MatchConfig : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> entity)
        {
            entity.HasKey(m => m.Id);

            entity.HasOne(m => m.Event)
                .WithMany(e => e.Matches)
                .HasForeignKey(m => m.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}