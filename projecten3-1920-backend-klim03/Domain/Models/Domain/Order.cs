using projecten3_1920_backend_klim03.Domain.Models.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Domain.Models.Domain
{
    public class Order
    {
        public long OrderId { get; set; }

        public DateTime Time { get; set; }
        public bool Submitted { get; set; } // when student indicates he finished his order
        public bool Approved { get; set; } // teacher approves the order 

        public long GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal GetOrderPrice => (decimal)OrderItems.Select(g => g.Product.Price * g.Amount).Sum();

        public decimal GetOrderScore => avgScore();

        public Order()
        {

        }

        public OrderItem AddOrderItem(OrderItem oi)
        {
            if (Submitted) throw new NotSupportedException("It is not allowed to change a finalized order");

            if(OrderItems.FirstOrDefault(g => g.ProductId == oi.ProductId) != null)
            {
                OrderItems.FirstOrDefault(g => g.ProductId == oi.ProductId).Amount++;
                return OrderItems.FirstOrDefault(g => g.ProductId == oi.ProductId);
            } else
            {
                OrderItems.Add(oi);
                return oi;
            }    
        }

        public void RemoveOrderItem(OrderItem oi)
        {
            if (Submitted) throw new NotSupportedException("It is not allowed to change a finalized order");
            OrderItems.Remove(oi);
        }

        public OrderItem GetOrderItemById(long id)
        {
            var ois = OrderItems.FirstOrDefault(g => g.OrderItemId == id);
            if (ois == null) throw new DomainArgumentNullException("Het gevraagde order item is niet gevonden");
            return ois;
        }


        public void SubmitOrder()
        {
            Group.PayOrder(GetOrderPrice);
            Submitted = true;
            Time = DateTime.Now;
        }


        public decimal avgScore()
        {
            int amount = 0;
            decimal total = 0;

            if (OrderItems.Count != 0) {

                foreach (var orderItem in OrderItems)
                {
                    for (int i = 0; i < orderItem.Amount; i++)
                    {
                        amount++;
                        total += orderItem.Product.Score;
                    }
                }

                return total / amount;
            } else
            {
                return 0;
            }
        }

    }
}
