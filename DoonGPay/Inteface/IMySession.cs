namespace DoonGPay.Inteface
{
    public interface IMySession
    {
        int? UserId { get; }
        string? FirstName { get; }
        string? LastName { get; }
    }
}
