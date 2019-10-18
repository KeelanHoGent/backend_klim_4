﻿using System;
using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;

namespace projecten3_1920_backend_klim03.Controllers
{
    [Route("api/[controller]")] // misschien niet nodig als je vanuit een groep gaat
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orders;

        public OrderController(IOrderRepo orders)
        {
            _orders = orders;
        }


        /// <summary>
        /// Adds an order item to an order
        /// </summary>
        /// <param name="orderId">the id of the order</param>
        /// <param name="dto">the orderitem to be added</param>
        /// <returns>The order</returns>
        [HttpPut("addOrderItem/{orderId}")]
        public ActionResult<OrderDTO> AddOrderItemToOrder([FromBody] OrderItemDTO dto, long orderId)
        {
            Order o = _orders.GetById(orderId);
            OrderItem oi = new OrderItem(dto);
            o.AddOrderItem(oi);
            _orders.SaveChanges();
            return new OrderDTO(o);
        }


        /// <summary>
        /// Removes an order item to an order
        /// </summary>
        /// <param name="orderId">the id of the order</param>
        /// <param name="orderItemId">the orderItem with the orderItemId to be removed</param>
        /// <returns>The order</returns>
        [HttpPut("removeOrderItem/{orderItemId}/{orderId}")]
        public ActionResult<OrderDTO> RemoveOrderItemFromOrder(long orderItemId, long orderId)
        {
            Order o = _orders.GetById(orderId);
            OrderItem oi = o.GetOrderItemById(orderItemId);
            o.RemoveOrderItem(oi);
            _orders.SaveChanges();
            return new OrderDTO(o);
        }


        /// <summary>
        /// Submits the final order
        /// </summary>
        /// <param name="orderId">the id of the order</param>
        /// <returns>The order</returns>
        [HttpPut("submitOrder/{rderId}")]
        public ActionResult<OrderDTO> SubmitOrder(long orderId)
        {
            Order o = _orders.GetById(orderId);
            o.Finalised = true;
            o.Time = DateTime.Now;
            _orders.SaveChanges();
            return new OrderDTO(o);
        }




    }
}
