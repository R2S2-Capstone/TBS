using System.Threading.Tasks;
using TBS.Data.Interfaces.User.Authentication;
using TBS.Data.Models.User.Authentication;

namespace TBS.Services.User.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<LoginResult> LoginAsync(string fireabaseId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<RegisterResult> RegisterAsync(string firebaseId)
        {
            throw new System.NotImplementedException();
        }
    }
}
