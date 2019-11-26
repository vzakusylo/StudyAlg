using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Usavc.WebMVC.ViewModels;

namespace Usavc.WebMVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<SelectListItem>> GetBrands();
    }
}
