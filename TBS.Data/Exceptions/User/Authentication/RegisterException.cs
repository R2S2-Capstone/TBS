using System;

namespace TBS.Data.Exceptions.User.Authentication
{
    public class RegisterException : Exception
    {
        public RegisterException() : base("Failed to register user")
        {

        }

        public RegisterException(string errorMessage) : base(errorMessage)
        {

        }
    }
}
