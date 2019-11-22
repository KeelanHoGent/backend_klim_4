using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class GroupRepo : IGroupRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Group> _groups;

        public GroupRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _groups = dbContext.Groups;
        }

        public void Add(Group obj)
        {
            _groups.Add(obj);
        }

        public ICollection<Group> GetAll()
        {
            return _groups.ToList();
        }


        public Group GetById(long id)
        {
            return _groups.Include(g => g.PupilGroups).ThenInclude(g => g.Pupil)
                .Include(g => g.Order).ThenInclude(g => g.OrderItems).ThenInclude(g => g.Product)
                .SingleOrDefault(g => g.GroupId == id);
        }

        public void Remove(Group obj)
        {
            _groups.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Group GetByUniqueGroupCodeWithOrder(string uniqueGroupCode)
        {
            return _groups.Include(g => g.Order).ThenInclude(g => g.OrderItems).ThenInclude(g => g.Product).ThenInclude(g => g.Category)
                .SingleOrDefault(g => g.UniqueGroupCode == uniqueGroupCode);
        }

        public Group GetByUniqueGroupCodeWithProjectAndOrder(string uniqueGroupCode)
        {
            return _groups
                .Include(g => g.Order).ThenInclude(g => g.OrderItems).ThenInclude(g => g.Product).ThenInclude(g => g.Category)
                .Include(g => g.Project).ThenInclude(g => g.Products).ThenInclude(g => g.Category)
                .Include(g => g.Project).ThenInclude(g => g.ApplicationDomain)
                .SingleOrDefault(g => g.UniqueGroupCode == uniqueGroupCode);
        }

        public Group GetByIdToAddEvaluation(long groupId)
        {
            return _groups
                 .SingleOrDefault(g => g.GroupId == groupId);
        }

        public Group GetByIdToEditEvaluation(long groupId)
        {
            return _groups.Include(g => g.Evaluations).ThenInclude(g => g.EvaluationCriterea)
               .SingleOrDefault(g => g.GroupId == groupId);
        }
    }
}
