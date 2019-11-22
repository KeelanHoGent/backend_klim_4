using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;

namespace projecten3_1920_backend_klim03.Data.Mapping
{
    public class ProductTemplateProjectTemplateConfig : IEntityTypeConfiguration<ProductTemplateProjectTemplate>
    {
        public void Configure(EntityTypeBuilder<ProductTemplateProjectTemplate> builder)
        {
            builder.ToTable("ProductTemplateProjectTemplate");

            builder.HasKey(ep => new { ep.ProductTemplateId, ep.ProjectTemplateId });
            builder.HasOne(ep => ep.ProductTemplate).WithMany(p => p.ProductTemplateProjectTemplates).HasForeignKey(ep => ep.ProductTemplateId);
            builder.HasOne(ep => ep.ProjectTemplate).WithMany(e => e.ProductTemplateProjectTemplates).HasForeignKey(ep => ep.ProjectTemplateId).OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
