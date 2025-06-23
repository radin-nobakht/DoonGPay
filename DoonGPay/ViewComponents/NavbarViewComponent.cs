using DoonGPay.Service;
using DoonGPay.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
