using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Mapping
{
    public class EvaluationCritereaConfig : IEntityTypeConfiguration<EvaluationCriterea>
    {
        public void Configure(EntityTypeBuilder<EvaluationCriterea> builder)
        {
            builder.ToTable("EvaluationCriterea");
            builder.HasKey(g => g.EvaluationCritereaId);

            builder.HasMany(g => g.Evaluations).WithOne(g => g.EvaluationCriterea).HasForeignKey(g => g.EvaluationCritereaId);

        }
    }
}
