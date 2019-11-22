using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class ProjectTemplateDTO
    {
        public long ProjectTemplateId { get; set; }

        public string ProjectName { get; set; }
        public string ProjectDescr { get; set; }
        public string ProjectImage { get; set; }
        public bool AddedByGO { get; set; }

        public long ApplicationDomainId { get; set; }
        public ApplicationDomainDTO ApplicationDomain { get; set; }

        public ICollection<ProductTemplateDTO> ProductTemplates { get; set; } = new List<ProductTemplateDTO>();
        public int MaxScore { get; internal set; }
        public int Budget { get; internal set; }

        public ProjectTemplateDTO()
        {

        }

        public ProjectTemplateDTO(ProjectTemplate pt)
        {
            ProjectTemplateId = pt.ProjectTemplateId;

            ProjectName = pt.ProjectName;
            ProjectDescr = pt.ProjectDescr;
            ProjectImage = pt.ProjectImage;
            AddedByGO = pt.AddedByGO;
            Budget = pt.Budget;
            MaxScore = pt.MaxScore;

            ApplicationDomainId = pt.ApplicationDomainId;
            if(pt.ApplicationDomain != null)
            {
                ApplicationDomain = new ApplicationDomainDTO(pt.ApplicationDomain);
            }
            

            ProductTemplates = pt.ProductTemplateProjectTemplates.Select(g => new ProductTemplateDTO(g.ProductTemplate)).ToList();

        }
    }
}
