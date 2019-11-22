using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs.CustomDTOs
{
    public class PdfSettings
    {
        public List<long> GroupsToShow { get; set; }

        public bool ShowPupil { get; set; }
        public bool ShowTeacher { get; set; }

        public PdfSettings()
        {

        }


    }
}
