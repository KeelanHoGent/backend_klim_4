using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain
{
    public class ProjectTemplate
    {
        public long ProjectTemplateId { get; set; }

        public string ProjectName { get; set; }
        public string ProjectDescr { get; set; }
        public string ProjectImage { get; set; }
        public bool AddedByGO { get; set; }
        public int Budget { get; set; }
        public int MaxScore { get; set; }
        public long ApplicationDomainId { get; set; }
        public ApplicationDomain ApplicationDomain { get; set; }

        public long SchoolId { get; set; }
        public School School { get; set; }

        public ICollection<ProductTemplateProjectTemplate> ProductTemplateProjectTemplates { get; set; } = new List<ProductTemplateProjectTemplate>();



        public ProjectTemplate()
        {
          
        }

        public ProjectTemplate(ProjectTemplateDTO dto, bool addedByGO)
        {
            ProjectName = dto.ProjectName;
            ProjectDescr = dto.ProjectDescr;
            ProjectImage = dto.ProjectImage;
            Budget = dto.Budget;
            MaxScore = dto.MaxScore;
            AddedByGO = addedByGO;
            ApplicationDomainId = dto.ApplicationDomainId;
            
            

        }


        public void AddProductTemplate(ProductTemplate pt)
        {
            ProductTemplateProjectTemplates.Add(new ProductTemplateProjectTemplate { 
                ProductTemplate = pt,
                ProjectTemplate = this 
            });
        }

        public void RemoveProductTemplate(ProductTemplateProjectTemplate ptpt)
        {
            ProductTemplateProjectTemplates.Remove(ptpt);
        }


        





    }
}
