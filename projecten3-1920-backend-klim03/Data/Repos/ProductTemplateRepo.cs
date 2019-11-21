using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class ProductTemplateRepo : IProductTemplateRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProductTemplate> _productTemplates;
        private readonly DbSet<CategoryTemplate> _categoryTemplates;

        public ProductTemplateRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _productTemplates = dbContext.ProductTemplates;
            _categoryTemplates = dbContext.CategoryTemplates;
        }

        public void Add(ProductTemplate obj)
        {
            _context.Add(obj);
        }

        public ICollection<ProductTemplate> GetAll()
        {
            return _productTemplates.ToList();
        }

        public ICollection<CategoryTemplate> GetAllCategories()
        {
            return _categoryTemplates.ToList();
        }

        public ProductTemplate GetById(long id)
        {
            throw new NotImplementedException();
        }

        public CategoryTemplate getCategoryById(long categoryTemplateId)
        {
            return _categoryTemplates.Where(c => c.CategoryTemplateId == categoryTemplateId).SingleOrDefault();
        }

        public void Remove(ProductTemplate obj)
        {
            _context.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
