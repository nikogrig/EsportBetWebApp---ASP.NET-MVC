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
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> entity)
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Sport)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.SportId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}