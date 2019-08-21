using System.Threading.Tasks;
using TBS.Data.Models.Users.Authentication.Request;
using TBS.Data.Models.Users.Authentication.Result;

namespace TBS.Data.Interfaces.Users.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResult> LoginAsync(LoginRequest login);
        Task<RegisterResult> RegisterAsync(RegisterRequest registerDto);
    }
}
