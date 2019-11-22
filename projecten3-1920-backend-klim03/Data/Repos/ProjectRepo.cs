using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class ProjectRepo : IProjectRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Project> _projects;

        public ProjectRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _projects = dbContext.Projects;
        }

        public void Add(Project obj)
        {
            _projects.Add(obj);
        }

        public ICollection<Project> GetAll()
        {
            return _projects.ToList();
        }



        public Project GetById(long id)
        {
            return _projects
                .Include(g => g.ClassRoom)
                .Include(g => g.EvaluationCritereas)
                .Include(g => g.Groups).ThenInclude(g => g.Evaluations)
                .Include(g => g.Products).ThenInclude(g => g.Category)
                .Include(g => g.Groups).ThenInclude(g => g.Order)
                .Include(g => g.Groups)
                .Include(g => g.ApplicationDomain)
                .SingleOrDefault(g => g.ProjectId == id);
        }

        public Project GetForProjectProgress(long id)
        {
            return _projects
             .Include(g => g.ClassRoom)
             .Include(g => g.EvaluationCritereas)
             .Include(g => g.Groups).ThenInclude(g => g.Evaluations).ThenInclude(g => g.EvaluationCriterea)
             .Include(g => g.Groups).ThenInclude(g => g.Order).ThenInclude(g => g.OrderItems).ThenInclude(g => g.Product).ThenInclude(g => g.Category)
             .Include(g => g.Groups).ThenInclude(g => g.PupilGroups).ThenInclude(g => g.Pupil)
             // evaluatie
             .Include(g => g.ApplicationDomain)
             .SingleOrDefault(g => g.ProjectId == id);
        }

        public Project GetWithGroupsById(long id)
        {
            return _projects
               .Include(g => g.ClassRoom)
               .Include(g => g.EvaluationCritereas)
               .Include(g => g.Groups).ThenInclude(g => g.Evaluations)
               .Include(g => g.Groups).ThenInclude(g => g.Order).ThenInclude(g => g.OrderItems).ThenInclude(g => g.Product).ThenInclude(g => g.Category)
               .Include(g => g.Groups).ThenInclude(g => g.PupilGroups).ThenInclude(g => g.Pupil)
               .Include(g => g.ApplicationDomain)
               .SingleOrDefault(g => g.ProjectId == id);
        }

        public Product GetProductFromProject(long projectId, long productId)
        {
            return _projects.Include(p => p.Products).ThenInclude(p => p.Category).SingleOrDefault(p => p.ProjectId == projectId).Products.SingleOrDefault(p => p.ProductId == productId);
        }

        public void Remove(Project obj)
        {
            _projects.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
