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
    public class SportConfig : IEntityTypeConfiguration<Sport>
    {
        public void Configure(EntityTypeBuilder<Sport> entity)
        {
            entity.HasKey(s => s.Id);
        }
    }
}