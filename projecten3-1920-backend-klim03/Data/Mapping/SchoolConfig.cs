using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten3_1920_backend_klim03.Domain.Models.Domain;

namespace projecten3_1920_backend_klim03.Data.Mapping
{
    public class SchoolConfig : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.ToTable("School");
            builder.HasKey(g => g.SchoolId);


            builder.HasMany(g => g.ClassRooms).WithOne(g => g.School).HasForeignKey(g => g.SchoolId);
            builder.HasMany(g => g.Pupils).WithOne(g => g.School).HasForeignKey(g => g.SchoolId);

            builder.HasMany(g => g.ProjectTemplates).WithOne(g => g.School).HasForeignKey(g => g.SchoolId);
            builder.HasMany(g => g.ProductTemplates).WithOne(g => g.School).HasForeignKey(g => g.SchoolId);

            builder.HasOne(g => g.Adres).WithOne().HasForeignKey<School>(g => g.AdresId);

        }
    }
}
