using SportsStore.Models;
using Xunit;
using System.Linq;
namespace SportsStore.Tests
{
    public class CartTest
    {
        [Fact]
        public void Can_Add_New_Line(){
            //Arrange
            Product p1 = new Product{
                ProductID=1,
                Name="P1"
            };
            Product p2 = new Product{
                ProductID = 2,
                Name = "P2"
            };
            //Act
            Cart target = new Cart();
            target.AddItem(p1,1);
            target.AddItem(p2,1);
            CartLine[] results = target.Lines.ToArray();
            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);

        }
        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Arange
            Product p1 = new Product{ProductID =1, Name="P1" };
            Product p2 = new Product{ProductID =2, Name="P2"};
            Cart target = new Cart();

            target.AddItem(p1,1);
            target.AddItem(p2,2);
            target.AddItem(p1,10);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();
            //Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }
       [Fact]
        public void Can_Remove_Quantity_For_Existing_Lines()
        {
            //Arange
            Product p1 = new Product{ProductID =1, Name="P1" };
            Product p2 = new Product{ProductID =2, Name="P2"};
            Cart target = new Cart();

            target.AddItem(p1,1);
            target.AddItem(p2,2);
            target.AddItem(p1,10);

            target.RemoveLine(p1);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();
           
            //Assert
            Assert.Empty(target.Lines.Where(l=>l.Product == p1));
            Assert.Equal(2, target.Lines.Count()); 
        } [Fact]
        public void Can_Calculate_Sum_For_Existing_Lines()
        {
            //Arange
            Product p1 = new Product{ProductID =1, Name="P1" , Price=1};
            Product p2 = new Product{ProductID =2, Name="P2" , Price=1};
            Cart target = new Cart();

            target.AddItem(p1,1);
            target.AddItem(p2,2);
            target.AddItem(p1,10);

       
            Assert.Equal(13m,target.ComputeTotalValue());
        }
        [Fact]
        public void Can_Clear_Items()
        {
           Product p1 = new Product{ProductID =1, Name="P1" , Price=1};
            Product p2 = new Product{ProductID =2, Name="P2" , Price=1};
            Cart target = new Cart();

            target.AddItem(p1,1);
            target.AddItem(p2,2); 
            target.Clear();

            Assert.Empty( target.Lines );

        }
    }
}