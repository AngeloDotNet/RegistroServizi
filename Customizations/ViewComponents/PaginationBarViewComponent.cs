using Microsoft.AspNetCore.Mvc;
using RegistroServizi.Models.ViewModels;

namespace RegistroServizi.Customizations.ViewComponents
{
    public class PaginationBarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPaginationInfo model)
        {
            return View(model);
        }
    }
}