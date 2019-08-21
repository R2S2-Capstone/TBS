using TBS.Data.Models.Users.Information;

namespace TBS.Data.Models.Users.Authentication.Request
{
    public class RegisterRequest
    {
        public string UserFirebaseId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Company Company { get; set; }
        public AccountType AccountType { get; set; }
        public string DealerNumber { get; set; }
        public string RIN { get; set; }
        public string DriversLicense { get; set; }
    }
}
