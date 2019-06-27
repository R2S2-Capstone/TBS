namespace TBS.Data.Models.User.Authentication.Request
{
    public class RegisterRequest
    {
        public string UserFirebaseId { get; set; }
        public AccountType AccountType { get; set; }
        public string Email { get; set; }
    }
}
