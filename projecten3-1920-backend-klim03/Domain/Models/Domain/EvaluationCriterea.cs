using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain
{
    public class EvaluationCriterea
    {
        public long EvaluationCritereaId { get; set; }
        public string Title { get; set; }


        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();


        public EvaluationCriterea()
        {

        }


        public EvaluationCriterea(EvaluationCritereaDTO dto)
        {
            Title = dto.Title;
        }

    }
}
