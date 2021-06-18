using Microsoft.AspNetCore.Mvc;
using SwimmingStore.Models.Repository;
using SwimmingStore.Models;

namespace SwimmingStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _repository;

        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Products);
        }
    }
}
