using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Domain.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class ProjectDTO
    {
        public long ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectDescr { get; set; }
        public string ProjectImage { get; set; }
        [Required]
        public decimal ProjectBudget { get; set; }
        [Required]
        public ESchoolGrade ESchoolYear { get; set; }
        [Required]
        public long ClassRoomId { get; set; }

        public bool Closed { get; set; }

        [Required]
        public long ApplicationDomainId { get; set; }
        public ApplicationDomainDTO ApplicationDomain { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public ICollection<GroupDTO> Groups { get; set; } = new List<GroupDTO>();
        public ICollection<EvaluationCritereaDTO> EvaluationCritereas { get; set; } = new List<EvaluationCritereaDTO>();

        public ProjectDTO()
        {

        }

        public ProjectDTO(Project project)
        {
            ProjectId = project.ProjectId;

            ProjectName = project.ProjectName;
            ProjectDescr = project.ProjectDescr;
            ProjectImage = project.ProjectImage;
            ESchoolYear = project.ESchoolGrade;
            ProjectBudget = project.ProjectBudget;
            Closed = project.Closed;

            ClassRoomId = project.ClassRoomId;
            ApplicationDomainId = project.ApplicationDomainId;

            if(project.ApplicationDomain != null)
            {
                ApplicationDomain = new ApplicationDomainDTO(project.ApplicationDomain);
            }
            

            Products = project.Products.Select(g => new ProductDTO(g)).ToList();
            Groups = project.Groups.Select(g => new GroupDTO(g)).ToList();

            EvaluationCritereas = project.EvaluationCritereas.Select(g => new EvaluationCritereaDTO(g)).ToList();
        }



    }
}
