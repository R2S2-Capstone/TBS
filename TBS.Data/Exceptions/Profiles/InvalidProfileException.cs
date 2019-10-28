using System;

namespace TBS.Data.Exceptions.Profiles
{
    public class InvalidProfileException : Exception
    {
        public InvalidProfileException() : base("Invalid profile")
        {

        }

        public InvalidProfileException(string message) : base(message)
        {

        }
    }
}
