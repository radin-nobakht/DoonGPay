using DoonGPay.Inteface;
using DoonGPay.Service;
using DoonGPay.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.ViewComponents
{
    public class UserViewComponent (IMySession mySession): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(new UserViewModel {IsLogin=mySession.IsLogin ,Name=mySession.FullName,Image=mySession.ImageStr() });
        }
    }
}
