using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Interfaces
{
    public interface ISchoolRepo : IGenericRepo<School>
    {
        School GetByIdWithTemplates(long id);
        School GetByIdWithClassrooms(long id);
    }
}
