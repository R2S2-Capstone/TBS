namespace TBS.Data.Interfaces.User.Authentication
{
    public interface IAuthenticationService
    {
        void Login(string fireabaseId);
        void Register(string firebaseId);
    }
}
