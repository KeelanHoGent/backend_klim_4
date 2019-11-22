using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Order> _orders;

        public OrderRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _orders = dbContext.Orders;
        }

        public void Add(Order obj)
        {
            _orders.Add(obj);
        }

        public ICollection<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(long id)
        {
            return _orders.Include(g => g.OrderItems).ThenInclude(g => g.Product).ThenInclude(g => g.Category)
               .SingleOrDefault(g => g.OrderId == id);
        }

        public Order GetByIdWithGroup(long id)
        {
            return _orders.Include(g => g.Group).ThenInclude(g => g.Project).Include(g => g.OrderItems).ThenInclude(g => g.Product).SingleOrDefault(g => g.OrderId == id);
        }

        public void Remove(Order obj)
        {
            _orders.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
