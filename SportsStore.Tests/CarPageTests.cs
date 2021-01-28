using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using SportsStore.Models;
using SportsStore.Pages;
using System.Linq;
using System.Text;
using System.Text.Json;
using Xunit;

namespace SportsStore.Tests
{
    
    public class CarPageTests
    {

        [Fact]
        public void Can_Load_Cart()
        {
        Product p1 = new Product{
            ProductID = 1,
            Name = "P1"
        };
        Product p2 = new Product{
            ProductID = 1,
            Name = "P1"
        };

        

        mockRepo.Setup( m => m.Products )
        .Returns(( new Product[]{   p1, p2  })
        .AsQueryable<Product>());
        Cart testCart = new Cart();
        testCart.AddItem(p1,2);
        testCart.AddItem(p2,1);
         Mock<ISession> mockSession = new Mock<ISession>();
        Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
        mockRepo.Setup(m => m.Products).Returns((
            new Product[]{
                    new Product{
                         ProductID=1,
                         Name="P1"
                    },
                    new Product{
                        ProductID = 2,
                        Name="P2"
                    }

        }).AsQueryable());

        Cart testCart = new Cart();
        testCart.AddItem(p1,2);
        testCart.AddItem(p2,1);
        CartModel cartModel = new CartModel(mockRepo.Object, testCart);
        cartModel.OnGet("myUrl");

        Assert.Equal(2, cartModel.Cart.Lines.Count());
        Assert.Equal("myurl", cartModel.ReturnUrl);
        //  byte[] data=Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
        //  mockSession.Setup(c =>
        //      c.TryGetValue(It.IsAny<string>(), out data)); 
        //  Mock<HttpContext> mockContext = new Mock<HttpContext>();
        //  mockContext.SetupGet(c => c.Session).Returns(mockSession.Object);
        //  CartModel cartModel = new CartModel(mockRepo.Object)
        //  {
        //      PageContext = new PageContext(
        //                     new ActionContext{
        //                           HttpContext = mockContext.Object,
        //                           RouteData= new Microsoft.AspNetCore.Routing.RouteData(),
        //                           ActionDescriptor = new PageActionDescriptor()
        //                       })
        //  };

        // cartModel.OnGet("MyUrl");
        // Assert.Equal(2, cartModel.Cart.Lines.Count() );

        // Assert.Equal("myUrl",cartModel.ReturnUrl );
        
    }

    [Fact]
    public void Can_Update_Cart()
    {
        //Arange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products)
        .Returns((new Product[]{
                new Product{ProductID =1, Name="P1"} 

        }).AsQueryable<Product>());
        Cart testCart = new Cart();

        CartModel cartModel = new CartModel(mock.Object,testCart);
        cartModel.OnPost(1,"myUrl");
        Assert.Single(testCart.Lines);
        Assert.Equal("P1", testCart.Lines.First().Product.Name);
        Assert.Equal(1, testCart.Lines.First().Quantity);
        // Mock<ISession> mockSession = new Mock<ISession>();
        // mockSession.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
        // .Callback<string,byte[]>((key,val)=>{
        //      testCart = JsonSerializer
        //                 .Deserialize<Cart>(Encoding.UTF8.GetString(val));
        // });
        // Mock<HttpContext> mockContext = new Mock<HttpContext>();
        // mockContext.SetupGet(c =>c.Session).Returns(mockSession.Object);
        // CartModel cartModel = new CartModel(mock.Object)
        // {
        //     PageContext = new PageContext(
        //             new ActionContext{
        //                           HttpContext = mockContext.Object,
        //                             RouteData = new Microsoft.AspNetCore.Routing.RouteData(),
        //                      ActionDescriptor = new PageActionDescriptor()
        //     })
        // };
        // cartModel.OnPost(1,"myUrl");
        // Assert.Single(testCart.Lines);
        // Assert.Equal(1, testCart.Lines.First().Quantity);
        // Assert.Equal("P1", testCart.Lines.First().Product.Name);

    }
}
}