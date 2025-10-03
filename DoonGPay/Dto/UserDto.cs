using DoonGPay.Dto.Travel;

namespace DoonGPay.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public  string UserName { get; set; }
        public  string FristName { get; set; }
        public  string LastName { get; set; }
        public  string Password { get; set; }
        public  string PhoneNumber { get; set; }
        public  string UserAvatar { get; set; }
        public  string TravelImage { get; set; }
        public virtual ICollection<TravelDto> Travels { get; set; }
        public virtual ICollection<UsuallyFriendDto> UsuallyFriends { get; set; }

    }
}
