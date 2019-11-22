using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs.AppDTOs
{
    public class AppProjectDTO
    {

        public long ProjectId { get; set; }

        public string ProjectName { get; set; }
        public string ProjectDescr { get; set; }
        public string ProjectImage { get; set; }
        public decimal ProjectBudget { get; set; }
        public long ClassRoomId { get; set; }

        public bool Closed { get; set; }

        public long ApplicationDomainId { get; set; }
        public ApplicationDomainDTO ApplicationDomain { get; set; }

        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();


        public AppProjectDTO()
        {

        }

        public AppProjectDTO(Project project)
        {
            ProjectId = project.ProjectId;

            ProjectName = project.ProjectName;
            ProjectDescr = project.ProjectDescr;
            ProjectImage = project.ProjectImage;
            ProjectBudget = project.ProjectBudget;

            Closed = project.Closed;

            ClassRoomId = project.ClassRoomId;
            ApplicationDomainId = project.ApplicationDomainId;
            ApplicationDomain = new ApplicationDomainDTO(project.ApplicationDomain);

            Products = project.Products.Select(g => new ProductDTO(g)).ToList();

        }
    }
}
