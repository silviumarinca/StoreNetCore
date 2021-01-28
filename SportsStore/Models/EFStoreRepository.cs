using System.Linq; 

namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;
        public EFStoreRepository(StoreDbContext ctx)
        {
            this.context = ctx; 
        }
        public IQueryable<Product> Products => context.Products;

        public void CreateProduct(Product p)
        {
           this.context.Add(p);
           this.context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            this.context.Remove(p);
           this.context.SaveChanges();
        }

        public void SaveProduct(Product p)
        {
              this.context.SaveChanges();
        }
    }
}