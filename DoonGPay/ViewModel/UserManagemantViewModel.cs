using DoonGPay.Dto;
using DoonGPay.Dto.Travel;

namespace DoonGPay.ViewModel
{
    public class UserManagemantViewModel
    {
        public PagationViewModel<UserDto> UserPgation { get; set; }
        public TravelDto Travel { get; set; }
    }
}
