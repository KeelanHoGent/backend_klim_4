using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class PupilDTO
    {
        public long PupilId { get; set; }


        public string FirstName { get; set; }
        public string Surname { get; set; }



        public PupilDTO()
        {

        }

        public PupilDTO(Pupil pupil)
        {
            PupilId = pupil.PupilId;
            FirstName = pupil.FirstName;
            Surname = pupil.Surname;
            //schoolId ook ?
        }
    }
}
