using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.DTOs;
using projecten3_1920_backend_klim03.Domain.Models.DTOs.CustomDTOs;
using projecten3_1920_backend_klim03.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projecten3_1920_backend_klim03.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepo _orderItems;

        public OrderItemController(IOrderItemRepo orderItems)
        {
            _orderItems = orderItems;
        }



        /// <summary>
        /// Changes the amount of a given orderItem
        /// </summary>
        /// <param name="dto">The modified order item</param>
        /// <param name="orderItemId">The id of the order item</param>
        /// <returns>The order item</returns>
        [AllowAnonymous]
        [HttpPut("{orderItemId}")]
        public ActionResult<RemoveOrAddedOrderItemDTO> PutOrderItem([FromBody]OrderItemDTO dto, long orderItemId)
        {
            try
            {
                OrderItem oi = _orderItems.GetById(orderItemId);
                oi.Amount = dto.Amount;
                _orderItems.SaveChanges();

                return new RemoveOrAddedOrderItemDTO(oi.Order, oi);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Order item niet gevonden"));
            }

        }

        /// <summary>
        /// Substract one from the amount in order item
        /// </summary>
        /// <param name="dto">The modified order item</param>
        /// <param name="orderItemId">The id of the order item</param>
        /// <returns>The order item</returns>
        [AllowAnonymous]
        [HttpPut("substractOne/{orderItemId}")]
        public ActionResult<RemoveOrAddedOrderItemDTO> substractOrderByOne(long orderItemId)
        {
            try
            {
                OrderItem oi = _orderItems.GetById(orderItemId);
                oi.SubstractOne();
                _orderItems.SaveChanges();

                return new RemoveOrAddedOrderItemDTO(oi.Order, oi);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Order item niet gevonden"));
            }

        }


        /// <summary>
        /// Substract one from the amount in order item
        /// </summary>
        /// <param name="dto">The modified order item</param>
        /// <param name="orderItemId">The id of the order item</param>
        /// <returns>The order item</returns>
        [AllowAnonymous]
        [HttpPut("addOne/{orderItemId}")]
        public ActionResult<RemoveOrAddedOrderItemDTO> AddOrderItemByOne(long orderItemId)
        {
            try
            {
                OrderItem oi = _orderItems.GetById(orderItemId);
                oi.AddOne();
                _orderItems.SaveChanges();

                return new RemoveOrAddedOrderItemDTO(oi.Order, oi);
            }
            catch (ArgumentNullException)
            {
                return NotFound(new CustomErrorDTO("Order item niet gevonden"));
            }

        }
    }
}
