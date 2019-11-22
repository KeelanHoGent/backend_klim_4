using System;
using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.CustomExceptions;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.DTOs.CustomDTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;

namespace projecten3_1920_backend_klim03.Controllers
{
    [Route("api/[controller]")] // misschien niet nodig als je vanuit een groep gaat
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orders;
        private readonly IOrderItemRepo _orderItems;

        public OrderController(IOrderRepo orders, IOrderItemRepo orderItems)
        {
            _orders = orders;
            _orderItems = orderItems;
        }


        /// <summary>
        /// Adds an order item to an order
        /// </summary>
        /// <param name="orderId">the id of the order</param>
        /// <param name="dto">the orderitem to be added</param>
        /// <returns>The order</returns>
        [HttpPut("addOrderItem/{orderId}")]
        public ActionResult<RemoveOrAddedOrderItemDTO> AddOrderItemToOrder([FromBody] OrderItemDTO dto, long orderId)
        {
            try
            {
                Order o = _orders.GetById(orderId);

                OrderItem oi = new OrderItem(dto);
                var oiNew = o.AddOrderItem(oi);

                _orders.SaveChanges();

                var oiFullInclude = _orderItems.GetById(oiNew.OrderItemId);

                return new RemoveOrAddedOrderItemDTO(o, oiFullInclude);
            }
            catch(NotSupportedException ex)
            {
                return BadRequest(new CustomErrorDTO(ex.Message));
            }
            catch(ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Order niet gevonden"));
            } 
        }


        /// <summary>
        /// Removes an order item to an order
        /// </summary>
        /// <param name="orderId">the id of the order</param>
        /// <param name="orderItemId">the orderItem with the orderItemId to be removed</param>
        /// <returns>The order</returns>
        [HttpPut("removeOrderItem/{orderItemId}/{orderId}")]
        public ActionResult<RemoveOrAddedOrderItemDTO> RemoveOrderItemFromOrder(long orderItemId, long orderId)
        {
            try
            {
                Order o = _orders.GetById(orderId);

                OrderItem oi = o.GetOrderItemById(orderItemId);
                o.RemoveOrderItem(oi);

                _orders.SaveChanges();
                return new RemoveOrAddedOrderItemDTO(o, oi); 
            }
            catch(NotSupportedException ex)
            {
                return BadRequest(new CustomErrorDTO(ex.Message));
            }
            catch(DomainArgumentNullException ex)
            {
                return NotFound(new CustomErrorDTO(ex.Message));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Order niet gevonden"));
            }

        }


        /// <summary>
        /// Submits the final order
        /// </summary>
        /// <param name="orderId">the id of the order</param>
        /// <returns>The order</returns>
        [HttpPut("submitOrder/{orderId}")]
        public ActionResult<OrderDTO> SubmitOrder(long orderId)
        {
            try
            {
                Order o = _orders.GetById(orderId);
                o.SubmitOrder();

                _orders.SaveChanges();
                return new OrderDTO(o);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Orderamount <= 0
                return BadRequest(new CustomErrorDTO(ex.Message));
            }
            catch (ArithmeticException ex)
            {
                // Remaining budget < orderamount
                return BadRequest(new CustomErrorDTO(ex.Message));
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Order niet gevonden"));
            }    
        }

        /// <summary>
        /// Teacher approves the finalized order
        /// </summary>
        /// <param name="orderId">the id of the order</param>
        /// <returns>The order</returns>
        [HttpPost("approveOrder/{orderId}")]
        public ActionResult<OrderDTO> ApproveOrder(long orderId)
        {
            try
            {
                Order o = _orders.GetByIdWithGroup(orderId);
                o.Approved = true;

                _orders.SaveChanges();
                return new OrderDTO(o);
            }
         
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Order niet gevonden"));
            }
        }
    }
}
