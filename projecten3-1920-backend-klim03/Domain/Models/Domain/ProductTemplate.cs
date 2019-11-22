using projecten3_1920_backend_klim03.Domain.Models.Domain.ManyToMany;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain
{
    public class ProductTemplate
    {
        public long ProductTemplateId { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public bool HasMultipleDisplayVariations { get; set; }
        public int Score { get; set; }

        public bool AddedByGO { get; set; }

        public long CategoryTemplateId { get; set; }
        public CategoryTemplate CategoryTemplate { get; set; }

        public long SchoolId { get; set; }
        public School School { get; set; }

        public ICollection<ProductVariationTemplate> ProductVariationTemplates { get; set; } = new List<ProductVariationTemplate>();

        public ICollection<ProductTemplateProjectTemplate> ProductTemplateProjectTemplates { get; set; } = new List<ProductTemplateProjectTemplate>();

        public ProductTemplate()
        {

        }

        public ProductTemplate(ProductTemplateDTO dto, bool addedByGO)
        {
         
            ProductName = dto.ProductName;
            Description = dto.Description;
            ProductImage = dto.ProductImage;
            Score = dto.Score;
            
            AddedByGO = addedByGO;
        }

        public void RemoveVariation(ProductVariationTemplate pvt)
        {
            ProductVariationTemplates.Add(pvt);
        }

        public void AddVariation(ProductVariationTemplate pvt)
        {
            ProductVariationTemplates.Remove(pvt);
        }

        internal void UpdateVariations(ICollection<ProductVariationTemplate> pvts)
        {
            
            foreach (var item in ProductVariationTemplates.ToList())
            {
                var templateMatch = pvts.FirstOrDefault(g => g.ProductVariationTemplateId == item.ProductVariationTemplateId);
                if (templateMatch == null)
                {
                    RemoveVariation(item);
                }
                else 
                {
                    item.ProductDescr = templateMatch.ProductDescr;
                    item.ESchoolGrade = templateMatch.ESchoolGrade;
                }
            }

            foreach (var item in pvts)
            {
                if (item.ProductVariationTemplateId == 0)
                {
                    AddVariation(new ProductVariationTemplate(item.ProductDescr, item.ESchoolGrade));
                }
            }
        }
    }
}
