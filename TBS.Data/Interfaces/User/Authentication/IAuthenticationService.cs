using System.Threading.Tasks;
using TBS.Data.Models.User.Authentication;

namespace TBS.Data.Interfaces.User.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResult> LoginAsync(string fireabaseId);
        Task<RegisterResult> RegisterAsync(string firebaseId);
    }
}
