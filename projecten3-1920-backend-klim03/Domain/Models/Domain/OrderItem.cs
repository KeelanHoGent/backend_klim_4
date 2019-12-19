using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain
{
    public class OrderItem
    {

        public long OrderItemId { get; set; }

        public long Amount { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }


        public OrderItem()
        {

        }


        public OrderItem(OrderItemDTO dto)
        {
            Amount = dto.Amount;
            ProductId = dto.ProductId;
        }

        public void SubstractOne()
        {
            if (Order.Submitted) Order.Submitted = false;
            Amount--;
        }

        public void AddOne()
        {
            if (Order.Submitted) Order.Submitted = false;
            Amount++;
        }
    }
}
