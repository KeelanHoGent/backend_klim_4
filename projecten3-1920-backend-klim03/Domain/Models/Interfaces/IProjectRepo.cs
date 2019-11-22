using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Interfaces
{
    public interface IProjectRepo : IGenericRepo<Project>
    {

        Project GetWithGroupsById(long id);
        Project GetForProjectProgress(long id);
        Product GetProductFromProject(long projectId, long productId);
    }
}
