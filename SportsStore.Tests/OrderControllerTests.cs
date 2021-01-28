using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();

            Order order = new Order();

            OrderController target = new OrderController(mock.Object, cart);
            ViewResult  result =target.CheckOut(order) as ViewResult;
            mock.Verify(mock=> mock.SaveOrder(It.IsAny<Order>()), Times.Never);
            //Assert

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);


        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(),1);
            OrderController target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error");
            ViewResult result = target.CheckOut(new Order()) as ViewResult;
            mock.Verify(m=>m.SaveOrder(It.IsAny<Order>()),Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }
        [Fact]
        public void Can_Checkout_And_Sumit_Order()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(),1);
            OrderController target = new OrderController(mock.Object, cart);

            RedirectToPageResult result= target.CheckOut(new Order()) as RedirectToPageResult;
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()),Times.Once);

            Assert.Equal("/Completed", result.PageName);
        }
        
    }
}