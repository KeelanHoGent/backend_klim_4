using System;
using projecten3_1920_backend_klim03.Domain.Models.Domain;

namespace projecten3_1920_backend_klim03.Domain.Models.Interfaces
{
    public interface IGroupRepo : IGenericRepo<Group>
    {
        Group GetByUniqueGroupCodeWithOrder(string uniqueGroupCode);
        Group GetByUniqueGroupCodeWithProjectAndOrder(string uniqueGroupCode);
        Group GetByIdToAddEvaluation(long groupId);
        Group GetByIdToEditEvaluation(long groupId);
    }
}
