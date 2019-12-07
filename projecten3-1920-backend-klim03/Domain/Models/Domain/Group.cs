using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;
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
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();

        public string GroupCode { get; set; } // this code is not unique so always use UniqueGroupCode
        public string UniqueGroupCode => GroupId.ToString() + GroupCode;

        public Group()
        {

        }

        public Group(GroupDTO dto, long schoolId)
        {
            GroupName = dto.GroupName;
            GroupCode = Guid.NewGuid().ToString().Substring(0, 4);

            if (dto.Pupils != null)
            {
                dto.Pupils.Where(g => g.FirstName != "").ToList().ForEach(g => AddPupil(new Pupil(g, schoolId)));
            }


            //evaluaties worden pas later toegevoegd
            /*if (dto.Evaluations != null)
            {
                dto.Evaluations.ToList().ForEach(g => AddEvaluation(new Evaluation(g)));
            }*/ // 





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

        public void AddEvaluation(Evaluation e)
        {
            Evaluations.Add(e);
        }

        public void RemoveEvaluationById(long evaluationId)
        {
            Evaluations.Remove(GetEvaluationById(evaluationId));
        }


        public Evaluation GetEvaluationById(long evaluationId)
        {
            return Evaluations.ToList().SingleOrDefault(g => g.EvaluationId == evaluationId);
        }

        public void InitOrder()
        {
            Order = new Order
            {
            };
        }

        public void PayOrder(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            decimal result = Project.ProjectBudget - amount;

            if (result < 0)
            {
                throw new ArithmeticException("Result can't be less than 0");
            }

            // only check for erros when "paying" an order, amount is calculated at runtime
        }

        public void UpdatePupilGroup(ICollection<PupilDTO> pupils, long schoolId)
        {
            foreach (var pupilGroup in PupilGroups)
            {
                var pupilMatch = pupils.FirstOrDefault(p => p.PupilId == pupilGroup.PupilId);
                // when there is no pupilMatch between the given collection and the db collection, the user deleted the pupil 
                if (pupilMatch == null) 
                {
                    PupilGroups.Remove(pupilGroup);
                }
            }

            //when there are pupils with ID 0 in the given list, the pupils in the group are new
            pupils.ToList().FindAll(np => np.PupilId == 0).ForEach(p =>
            {
                AddPupil(new Pupil(p, schoolId));
            });
        }

    }
}
