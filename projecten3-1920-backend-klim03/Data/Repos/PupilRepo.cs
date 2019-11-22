using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class PupilRepo : IPupilRepo
    {

        private readonly ApplicationDbContext _context;
        private readonly DbSet<Pupil> _pupils;

        public PupilRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _pupils = dbContext.Pupils;
        }


        public void Add(Pupil obj)
        {
            _pupils.Add(obj);
        }

        public ICollection<Pupil> GetAll()
        {
            return _pupils.ToList();
        }

        public Pupil GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Pupil GetByNameInSchool(string firstname, string surname, long schoolId)
        {
            return _pupils.FirstOrDefault(g => g.FirstName == firstname && g.Surname == surname && g.SchoolId == schoolId);
        }

        public bool PupilExistsInSchool(string firstname, string surname, long schoolId)
        {
            return _pupils.Any(g => g.FirstName == firstname && g.Surname == surname && g.SchoolId == schoolId);
        }

        public void Remove(Pupil obj)
        {
            _pupils.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
