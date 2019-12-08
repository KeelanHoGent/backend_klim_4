using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class ClassRoomRepo : IClassRoomRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<ClassRoom> _classRooms;

        public ClassRoomRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _classRooms = dbContext.ClassRooms;
        }

        public void Add(ClassRoom obj)
        {
            _classRooms.Add(obj);
        }

        public ICollection<ClassRoom> GetAll()
        {
            throw new NotImplementedException();
        }


        public ClassRoom GetById(long id)
        {
            return _classRooms    
                .Include(c => c.Pupils)
                .SingleOrDefault(g => g.ClassRoomId == id);
        }

        public ClassRoom GetByIdWithProjects(long id)
        {
            return _classRooms
               .Include(g => g.Projects).ThenInclude(g => g.ApplicationDomain)
               .SingleOrDefault(g => g.ClassRoomId == id);
        }

        public void Remove(ClassRoom obj)
        {
            _classRooms.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
