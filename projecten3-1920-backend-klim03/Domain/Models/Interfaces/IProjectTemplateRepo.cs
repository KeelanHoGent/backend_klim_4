using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Interfaces
{
    public interface IProjectTemplateRepo : IGenericRepo<ProjectTemplate>
    {
        ICollection<ProjectTemplate> GetAllBySchoolid(long schoolid);
    }
}
