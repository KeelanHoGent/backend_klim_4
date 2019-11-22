using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten3_1920_backend_klim03.Domain.Models.Domain;

namespace projecten3_1920_backend_klim03.Data.Mapping
{
    public class ProductTemplateConfig : IEntityTypeConfiguration<ProductTemplate>
    {
        public void Configure(EntityTypeBuilder<ProductTemplate> builder)
        {
            builder.ToTable("ProductTemplate");
            builder.HasKey(g => g.ProductTemplateId);

            builder.HasOne(g => g.CategoryTemplate).WithMany().HasForeignKey(g => g.CategoryTemplateId);
            builder.HasMany(g => g.ProductVariationTemplates).WithOne();
        }
    }
}
