using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class EvaluationCritereaDTO
    {

        public long EvaluationCritereaId { get; set; }
        public string Title { get; set; }

        public EvaluationCritereaDTO()
        {

        }

        public EvaluationCritereaDTO(EvaluationCriterea ec)
        {
            EvaluationCritereaId = ec.EvaluationCritereaId;
            Title = ec.Title;
        }
    }
}
