using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class ProductTemplateDTO
    {
        public long ProductTemplateId { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public int Score { get; set; }
        public bool AddedByGO { get; set; }

        public long CategoryTemplateId { get; set; }
        public CategoryTemplate CategoryTemplate { get; set; }

        public ICollection<ProductVariationTemplate> ProductVariationTemplates { get; set; } = new List<ProductVariationTemplate>();

        public bool HasMultipleDisplayVariations { get; set; }


        public ProductTemplateDTO()
        {

        }

        public ProductTemplateDTO(ProductTemplate pt)
        {
            ProductTemplateId = pt.ProductTemplateId;

            ProductName = pt.ProductName;
            Description = pt.Description;

            ProductImage = pt.ProductImage;
            Score = pt.Score;
            AddedByGO = pt.AddedByGO;

            CategoryTemplateId = pt.CategoryTemplateId;
            CategoryTemplate = pt.CategoryTemplate;

            ProductVariationTemplates = pt.ProductVariationTemplates;
            HasMultipleDisplayVariations = pt.HasMultipleDisplayVariations;
        }
    }
}
