using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Usavc.WebMVC.Services;
using Usavc.WebMVC.ViewModels.CatalogViewModels;

namespace Usavc.WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private ICatalogService _catalogSvc;

        public CatalogController(ICatalogService catalogSvc) => 
            _catalogSvc = catalogSvc;

        public async Task<IActionResult> Index([FromQuery]string errorMsg)
        {
            var vm = new IndexViewModel
            {
                Brands = await _catalogSvc.GetBrands()
            };

            ViewBag.BasketInoperativeMsg = errorMsg;

            return View(vm);
        }
    }
}