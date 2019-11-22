using projecten3_1920_backend_klim03.Domain.Models.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace projecten3_1920_backend_klim03.Domain.Models.DTOs
{
    public class OrderItemDTO
    {
        public long OrderItemId { get; set; }
        [Required]
        public long Amount { get; set; }
        [Required]
        public long ProductId { get; set; }
        public long OrderId { get; set; }

        public ProductDTO Product { get; set; }

        public OrderItemDTO()
        {
        }

        public OrderItemDTO(OrderItem oi) 
        {
            OrderItemId = oi.OrderItemId;

            Amount = oi.Amount;

            ProductId = oi.ProductId;
            OrderId = oi.OrderId;

            if(oi.Product != null)
            {
                Product = new ProductDTO(oi.Product);
            }
            
        }
    }
}
