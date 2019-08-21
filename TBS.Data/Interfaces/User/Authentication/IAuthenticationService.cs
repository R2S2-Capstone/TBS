using System.Threading.Tasks;
using TBS.Data.Models.User.Authentication.Request;
using TBS.Data.Models.User.Authentication.Result;

namespace TBS.Data.Interfaces.User.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResult> LoginAsync(LoginRequest login);
        Task<RegisterResult> RegisterAsync(RegisterRequest registerDto);
    }
}
