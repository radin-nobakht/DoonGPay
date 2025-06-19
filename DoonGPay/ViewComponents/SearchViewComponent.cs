using DoonGPay.Inteface;
using DoonGPay.Service;
using DoonGPay.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.ViewComponents
{
    public class SearchViewComponent (IMySession mySession): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(new UserViewModel());
        }
    }
}
