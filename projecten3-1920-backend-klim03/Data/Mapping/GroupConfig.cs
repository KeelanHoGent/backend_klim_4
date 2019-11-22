using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten3_1920_backend_klim03.Domain.Models.Domain;

namespace projecten3_1920_backend_klim03.Data.Mapping
{
    public class GroupConfig : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");
            builder.HasKey(g => g.GroupId);

            builder.HasOne(g => g.Order).WithOne(g => g.Group).HasForeignKey<Order>(g => g.GroupId);
            builder.HasMany(g => g.Evaluations).WithOne(g => g.Group).HasForeignKey(g => g.GroupId);

        }
    }
}
