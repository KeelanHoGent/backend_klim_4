using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class SchoolRepo : ISchoolRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<School> _schools;

        public SchoolRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _schools = dbContext.Schools;
        }

        public void Add(School obj)
        {
            _schools.Add(obj);
        }

        public ICollection<School> GetAll()
        {
            return _schools.ToList();
        }

        public School GetById(long id)
        {
            return _schools
               .SingleOrDefault(g => g.SchoolId == id);
        }

        public School GetByIdWithTemplates(long id)
        {
            return _schools
               .Include(g => g.ProjectTemplates).ThenInclude(g => g.ApplicationDomain)
               .Include(g => g.ProductTemplates).ThenInclude(g => g.ProductVariationTemplates)
               .Include(g => g.ProductTemplates).ThenInclude(g => g.CategoryTemplate)
               .SingleOrDefault(g => g.SchoolId == id);
        }

        public School GetByIdWithClassrooms(long id)
        {
            return _schools.Include(s => s.ClassRooms).ThenInclude(c => c.Pupils)
                .SingleOrDefault(s => s.SchoolId == id);
        }

        public void Remove(School obj)
        {
            _schools.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
