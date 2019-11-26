using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Usavc.WebMVC.ViewModels.CatalogViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<SelectListItem> Brands { get; set; }
    }
}
