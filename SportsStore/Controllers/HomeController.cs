using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Linq;
namespace SportsStore.Controllers
{
    public class HomeController: Controller
    {
        public int PageSize = 4;
        private IStoreRepository repository;
        public HomeController(IStoreRepository repo)
        {
            this.repository = repo;
        }
        public ViewResult Index(string category,int productPage = 1) => View(
            new ProductsListViewModel
            {
                Products =   repository.Products
                .Where(c=>category == null || c.Category==category)
                .OrderBy(p => p.ProductID)
                      .Skip((productPage-1)*PageSize)
                      .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Where(e=> e.Category == category|| category==null).Count()
                },
                CurrentCategory = category
                

            }
          
        );
        
    }
}