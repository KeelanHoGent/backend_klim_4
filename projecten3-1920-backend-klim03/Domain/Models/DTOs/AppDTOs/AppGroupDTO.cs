using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs.AppDTOs
{
    public class AppGroupDTO
    {
        public long GroupId { get; set; }

        public string GroupName { get; set; }
        public long ProjectId { get; set; }

        public OrderDTO Order { get; set; }

        public AppProjectDTO Project { get; set; }

        public string UniqueGroupCode { get; set; }

        public ICollection<PupilDTO> Pupils { get; set; }

        public AppGroupDTO()
        {

        }
        public AppGroupDTO(Group group)
        {
            GroupId = group.GroupId;

            GroupName = group.GroupName;
            ProjectId = group.ProjectId;
            UniqueGroupCode = group.UniqueGroupCode;
            if (group.Order != null)
            {
                Order = new OrderDTO(group.Order);
            }
            if (group.Project != null)
            {
                Project = new AppProjectDTO(group.Project);
            }
            Pupils = new List<PupilDTO>();
            foreach(PupilGroup pupilGroup in group.PupilGroups)
            {
                Pupils.Add(new PupilDTO(pupilGroup.Pupil));
            }
        }
    }
}
