using Microsoft.AspNetCore.Mvc;
using SwimmingStore.Models.Repository;
using SwimmingStore.Models;
using System.Linq;
using SwimmingStore.Models.VIewModels;

namespace SwimmingStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _repository;

        public int PageSize = 4;

        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index(int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = _repository.Products
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItem = _repository.Products.Count()
                }
            });
        }
    }
}
