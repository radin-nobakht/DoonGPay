using DoonGPay.Inteface;
using DoonGPay.Service;
using DoonGPay.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.ViewComponents
{
    public class LinksViewComponent (IMySession mySession): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
