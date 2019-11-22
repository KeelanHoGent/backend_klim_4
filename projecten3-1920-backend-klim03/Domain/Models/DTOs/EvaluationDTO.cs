using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class EvaluationDTO
    {
        public long EvaluationId { get; set; }

        public string Title { get; set; } // enkel gebruiken als extra == true

        public string DescriptionPrivate { get; set; }
        public string DescriptionPupil { get; set; }

        public bool Extra { get; set; } // deze die nog niet als standaard bij een project zijn geconfigureerd

        public long? EvaluationCritereaId { get; set; }



        public EvaluationDTO()
        {

        }


        public EvaluationDTO(Evaluation e)
        {
            EvaluationId = e.EvaluationId;

     
            DescriptionPrivate = e.DescriptionPrivate;
            DescriptionPupil = e.DescriptionPupil;
            Extra = e.Extra;


            EvaluationCritereaId = e.EvaluationCritereaId;
            if(e.EvaluationCriterea != null)
            {
                Title = e.EvaluationCriterea.Title;
            } else
            {
                Title = e.Title;
            }
            
        }
    }
}
