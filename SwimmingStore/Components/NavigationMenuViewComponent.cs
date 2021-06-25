using Microsoft.AspNetCore.Mvc;
using SwimmingStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwimmingStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepository _repository;

        public NavigationMenuViewComponent(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(_repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
