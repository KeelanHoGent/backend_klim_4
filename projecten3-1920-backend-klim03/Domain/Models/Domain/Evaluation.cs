using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain
{
    public class Evaluation
    {
        public long EvaluationId { get; set; }

        public string Title { get; set; } // enkel gebruiken als extra == true

        public string DescriptionPrivate { get; set; }
        public string DescriptionPupil { get; set; }

        public bool Extra { get; set; } // deze die nog niet als standaard bij een project zijn geconfigureerd

        public long? EvaluationCritereaId { get; set; }
        public EvaluationCriterea EvaluationCriterea { get; set; }

        public long GroupId { get; set; }
        public Group Group { get; set; }


        public Evaluation()
        {

        }

        public Evaluation(EvaluationDTO dto)
        {
            if(dto.Extra == true)
            {
                Title = dto.Title;
            }
           
            DescriptionPrivate = dto.DescriptionPrivate;
            DescriptionPupil = dto.DescriptionPupil;

            Extra = dto.Extra;
        }
    }
}
