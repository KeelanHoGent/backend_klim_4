﻿using System;
using System.Collections.Generic;
using System.Linq;
using projecten3_1920_backend_klim03.Domain.Models.Domain;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class GroupDTO
    {
        public long GroupId { get; set; }

        public string GroupName { get; set; }
        public long ProjectId { get; set; }

        public OrderDTO Order { get; set; }

        public string UniqueGroupCode { get; set; }

        public ICollection<PupilDTO> Pupils { get; set; } = new List<PupilDTO>();

        public GroupDTO()
        {

        }
        public GroupDTO(Group group)
        {
            GroupId = group.GroupId;

            GroupName = group.GroupName;
            ProjectId = group.ProjectId;
            UniqueGroupCode = group.UniqueGroupCode;
            if(group.Order != null)
            {
                Order = new OrderDTO(group.Order);
            }

            Pupils = group.PupilGroups.Select(g => new PupilDTO(g.Pupil)).ToList();

        }
    }
}
