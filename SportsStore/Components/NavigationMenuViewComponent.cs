using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepository repository;
        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            this.repository = repository;
        }
        public IViewComponentResult Invoke()
        {   var routeData= RouteData?.Values["category"];
            ViewBag.SelectedCategory = routeData;
            return View(repository.Products
            .Select(x=>x.Category)
            .Distinct()
            .OrderBy(x=>x));
        }
        
    }
}