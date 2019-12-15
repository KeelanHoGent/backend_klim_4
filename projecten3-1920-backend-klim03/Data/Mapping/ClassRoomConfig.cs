using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten3_1920_backend_klim03.Domain.Models.Domain;

namespace projecten3_1920_backend_klim03.Data.Mapping
{
    public class ClassRoomConfig : IEntityTypeConfiguration<ClassRoom>
    {
        public void Configure(EntityTypeBuilder<ClassRoom> builder)
        {
            builder.ToTable("ClassRoom");
            builder.HasKey(g => g.ClassRoomId);

            builder.HasMany(g => g.Pupils).WithOne(g => g.ClassRoom).HasForeignKey(g => g.ClassRoomId);
            builder.HasMany(g => g.Projects).WithOne(g => g.ClassRoom).HasForeignKey(g => g.ClassRoomId);
            builder.HasMany(c => c.Pupils).WithOne();
        }
    }
}
