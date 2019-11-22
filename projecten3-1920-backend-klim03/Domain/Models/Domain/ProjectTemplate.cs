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
            AddedByGO = addedByGO;
            Budget = dto.Budget;
            MaxScore = dto.MaxScore;
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


        public void UpdateProductTemplates(ICollection<ProductTemplateDTO> prts, bool addedByGo)
        {
            foreach (var item in ProductTemplateProjectTemplates.ToList())
            {
                var productTemplateMatch = prts.FirstOrDefault(g => g.ProductTemplateId == item.ProductTemplateId);
                if (productTemplateMatch == null) // the product has been removed by the user
                {
                    RemoveProductTemplate(item);
                }
                else // the product is still present in both arrays so update the product
                {
                    item.ProductTemplate.ProductName = productTemplateMatch.ProductName;
                    item.ProductTemplate.Description = productTemplateMatch.Description;
                    item.ProductTemplate.ProductImage = productTemplateMatch.ProductImage;
                    
                    item.ProductTemplate.CategoryTemplateId = productTemplateMatch.CategoryTemplateId;

                    item.ProductTemplate.UpdateVariations(productTemplateMatch.ProductVariationTemplates);

                }
            }

            foreach (var item in prts) // adds products that have not been assigned an ID yet (long is default 0)
            {
                if (item.ProductTemplateId == 0)
                {
                    AddProductTemplate(new ProductTemplate(item, addedByGo)); 
                }
            }
        }





    }
}
