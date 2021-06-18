using Microsoft.AspNetCore.Mvc;


namespace SwimmingStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
