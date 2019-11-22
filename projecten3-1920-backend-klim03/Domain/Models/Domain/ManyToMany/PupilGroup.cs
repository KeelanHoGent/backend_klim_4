using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany
{
    public class PupilGroup
    {
        public long PupilId { get; set; }
        public Pupil Pupil { get; set; }

        public long GroupId { get; set; }
        public Group Group { get; set; }

        public PupilGroup()
        {

        }


    }
}
