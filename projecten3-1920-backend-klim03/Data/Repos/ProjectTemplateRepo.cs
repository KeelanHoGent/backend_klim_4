using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class ProjectTemplateRepo : IProjectTemplateRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ProjectTemplate> _projectTemplates;

        public ProjectTemplateRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _projectTemplates = dbContext.ProjectTemplates;
        }

        public void Add(ProjectTemplate obj)
        {
            _projectTemplates.Add(obj);
        }

        public ICollection<ProjectTemplate> GetAll()
        {
            return _projectTemplates
                .Include(g => g.ApplicationDomain)
                .Include(g => g.ProductTemplateProjectTemplates).ThenInclude(g => g.ProductTemplate).ThenInclude(g => g.CategoryTemplate)
                .Include(g => g.ProductTemplateProjectTemplates).ThenInclude(g => g.ProductTemplate).ThenInclude(g => g.ProductVariationTemplates)
                .ToList();
        }

        public ProjectTemplate GetById(long id)
        {
            return _projectTemplates
                .Include(g => g.ApplicationDomain)
                .Include(g => g.ProductTemplateProjectTemplates).ThenInclude(g => g.ProductTemplate).ThenInclude(g => g.CategoryTemplate)
                .Include(g => g.ProductTemplateProjectTemplates).ThenInclude(g => g.ProductTemplate).ThenInclude(g => g.ProductVariationTemplates)
                .SingleOrDefault(g => g.ProjectTemplateId == id);
        }


        public void Remove(ProjectTemplate obj)
        {
            _projectTemplates.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        ICollection<ProjectTemplate> IProjectTemplateRepo.GetAllBySchoolid(long schoolid)
        {
            return _projectTemplates
                .Include(g => g.ApplicationDomain)
                .Include(g => g.ProductTemplateProjectTemplates).ThenInclude(g => g.ProductTemplate).ThenInclude(g => g.CategoryTemplate)
                .Include(g => g.ProductTemplateProjectTemplates).ThenInclude(g => g.ProductTemplate).ThenInclude(g => g.ProductVariationTemplates)
                .Where(g => g.SchoolId == schoolid)
                .ToList();
        }
    }
}
