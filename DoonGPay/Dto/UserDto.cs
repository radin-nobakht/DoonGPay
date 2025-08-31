using DoonGPay.Dto.Travel;

namespace DoonGPay.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public  string FristName { get; set; }
        public  string LastName { get; set; }
        public  string PhoneNumber { get; set; }
        public virtual ICollection<TravelDto> Travels { get; set; }
        public virtual ICollection<UsuallyFriendDto> UsuallyFriends { get; set; }

    }
}
