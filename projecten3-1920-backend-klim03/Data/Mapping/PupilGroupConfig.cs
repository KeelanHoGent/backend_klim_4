using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Mapping
{
    public class PupilGroupConfig : IEntityTypeConfiguration<PupilGroup>
    {
        public void Configure(EntityTypeBuilder<PupilGroup> builder)
        {
            builder.ToTable("PupilGroup");

            builder.HasKey(ep => new { ep.PupilId, ep.GroupId });
            builder.HasOne(ep => ep.Pupil).WithMany(p => p.PupilGroups).HasForeignKey(ep => ep.PupilId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(ep => ep.Group).WithMany(e => e.PupilGroups).HasForeignKey(ep => ep.GroupId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
