using Microsoft.EntityFrameworkCore;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Data.Repos
{
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<OrderItem> _orderItems;

        public OrderItemRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _orderItems = dbContext.OrderItems;
        }

        public void Add(OrderItem obj)
        {
            _orderItems.Add(obj);
        }

        public ICollection<OrderItem> GetAll()
        {
            return _orderItems
                .Include(g => g.Product)
                .ToList();
        }

        public OrderItem GetById(long id)
        {
            return _orderItems
                .Include(g => g.Order).ThenInclude(g => g.Group)
                .Include(g => g.Order).ThenInclude(g => g.OrderItems).ThenInclude(g => g.Product).ThenInclude(g => g.Category)
                .Include(g => g.Product).ThenInclude(g => g.Category)
                .SingleOrDefault(g => g.OrderItemId == id);
        }

        public void Remove(OrderItem obj)
        {
            _orderItems.Remove(obj);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
