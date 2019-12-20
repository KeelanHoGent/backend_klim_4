using projecten3_1920_backend_klim03.Domain.Models.Domain;
using projecten3_1920_backend_klim03.Domain.Models.CustomExceptions;
using projecten3_1920_backend_klim03.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace projecten3_1920_backend_klim03.Tests.Models
{
    public class OrderTest : IDisposable
    {
        private readonly DummyApplicationDbContext dummyContext;
        private Order testOrder;

        public OrderTest()
        {
            dummyContext = new DummyApplicationDbContext();
            testOrder = dummyContext.testOrder;
        }

        [Fact]
        public void Order_AddItem_AddsItem()
        {
            int amountBefore = testOrder.OrderItems.Count;

            testOrder.AddOrderItem(dummyContext.testOrderItem);

            Assert.Equal(amountBefore + 1, testOrder.OrderItems.Count);
        }

        [Fact]
        public void Order_RemoveItem_RemovesItem()
        {
            int amountBefore = testOrder.OrderItems.Count;

            testOrder.RemoveOrderItem(testOrder.OrderItems.First());

            Assert.Equal(amountBefore - 1, testOrder.OrderItems.Count);
        }

        [Fact]
        public void Order_GetOrderItemById_CorrectItem()
        {
            OrderItem it = testOrder.GetOrderItemById(1);

            Assert.NotNull(it);
            Assert.Equal("Karton", it.Product.ProductName);
            Assert.Equal(2, it.Amount);
            Assert.Equal(5, it.Product.Price);
        }

        [Fact]
        public void Order_GetOrderItemByNonExistantId_ThrowsError()
        {
            Assert.Throws<DomainArgumentNullException>(() => testOrder.GetOrderItemById(100));
        }

        [Fact]
        public void Order_CheckOrderPrice_CorrectPrice()
        {
            Assert.Equal(Convert.ToDecimal(55.5), testOrder.GetOrderPrice, 2);
        }

        [Fact]
        public void Order_GetOrderPrice_OrderPriceUpdatesWhenAddingItem()
        {
            decimal startPrice = testOrder.GetOrderPrice;

            testOrder.AddOrderItem(dummyContext.testOrderItem);

            decimal newOrderTotal = startPrice + Convert.ToDecimal(dummyContext.testOrderItem.Product.Price * dummyContext.testOrderItem.Amount);
            Assert.Equal(newOrderTotal, testOrder.GetOrderPrice);
        }

        [Fact]
        public void Order_GetOrderPrice_OrderPriceUpdatesWhenRemovingItem()
        {
            decimal startPrice = testOrder.GetOrderPrice;

            OrderItem oi = dummyContext.testOrder.OrderItems.First();
            testOrder.RemoveOrderItem(oi);

            decimal newOrderTotal = startPrice - Convert.ToDecimal(oi.Product.Price * oi.Amount);
            Assert.Equal(newOrderTotal, testOrder.GetOrderPrice);
        }

       [Fact]
       public void Order_AddItem_submittedResetsAfterAdding()
        {
            testOrder.SubmitOrder();

            Assert.True(testOrder.Submitted);

            testOrder.AddOrderItem(dummyContext.testOrderItem);

            Assert.False(testOrder.Submitted);
        }

        [Fact]
        public void Order_AddItem_submittedResetsAfterRemoving()
        {
            testOrder.SubmitOrder();

            Assert.True(testOrder.Submitted);

            testOrder.AddOrderItem(testOrder.OrderItems.First());

            Assert.False(testOrder.Submitted);
        }

        [Fact]
        public void Order_RemoveAllOrderItems_removesAllOrderItems()
        {
            Assert.NotEqual(0, testOrder.OrderItems.Count);

            testOrder.RemoveAllOrderItems();

            Assert.Equal(0, testOrder.OrderItems.Count);
        }

        public void Dispose()
        {
            testOrder = dummyContext.testOrder;
        }
    }
}
