using DoonGPay.Dto;
using DoonGPay.Dto.Travel;

namespace DoonGPay.ViewModel
{
    public class UserManagemantViewModel
    {
        public PagationViewModel<UserDto> UserPgation { get; set; }
        public List<UserDto> User { get; set; }
    }
}
