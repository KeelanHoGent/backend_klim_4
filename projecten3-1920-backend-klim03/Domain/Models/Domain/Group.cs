﻿using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain
{
    public class Group
    {
        public long GroupId { get; set; }

        public string GroupName { get; set; }

        public Order Order { get; set; }

        public decimal AmountSpend => Project.ProjectBudget - Order.GetOrderPrice;

        public long ProjectId { get; set; }
        public Project Project { get; set; }

        public ICollection<PupilGroup> PupilGroups { get; set; } = new List<PupilGroup>();

        public string GroupCode { get; set; } // this code is not unique so always use UniqueGroupCode
        public string UniqueGroupCode => GroupId.ToString() + GroupCode;

        public Group()
        {
          
        }

        public Group(GroupDTO dto, long schoolId)
        {
            GroupName = dto.GroupName;
            GroupCode = Guid.NewGuid().ToString().Substring(0,4);

            dto.Pupils.ToList().ForEach(g => AddPupil(new Pupil(g, schoolId)));

            InitOrder();
        }

        public void AddPupil(Pupil p)
        {
            PupilGroups.Add(new PupilGroup
            {
                Pupil = p,
                Group = this
            });
        }




        public void InitOrder()
        {
            Order = new Order
            {   
            };
        }

        public void PayOrder(decimal amount)
        {
            if(amount <= 0)
            {
                throw new ArgumentOutOfRangeException("Amount needs to be greater than 0");
            }

            decimal result = Project.ProjectBudget - amount;

            if(result < 0)
            {
                throw new ArithmeticException("Result can't be less than 0");
            }

            // only check for erros when "paying" an order, amount is calculated at runtime
        }

    }
}
