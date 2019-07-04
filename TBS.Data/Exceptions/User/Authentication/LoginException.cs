using System;

namespace TBS.Data.Exceptions.User.Authentication
{
    public class LoginException : Exception
    {
        public LoginException() : base("Failed to login")
        {

        }

        public LoginException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
