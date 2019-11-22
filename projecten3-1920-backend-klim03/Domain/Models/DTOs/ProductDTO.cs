using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class ProductDTO
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }

        public long ProjectId { get; set; }

        public double Price { get; set; }
        public int Score { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public ProductDTO()
        {
        }
        public ProductDTO(Product product)
        {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            Description = product.Description;
            ProductImage = product.ProductImage;

            Price = product.Price;
            Score = product.Score;

            ProjectId = product.ProjectId;
            CategoryId = product.CategoryId;
            Category = product.Category;

        }
    }
}
