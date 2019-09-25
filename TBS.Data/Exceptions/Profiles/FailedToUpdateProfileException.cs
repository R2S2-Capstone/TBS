using System;

namespace TBS.Data.Exceptions.Profiles
{
    public class FailedToUpdateProfileException : Exception
    {
        public FailedToUpdateProfileException() : base("Failed to update profile")
        {

        }

        public FailedToUpdateProfileException(string message) : base(message)
        {

        }
    }
}
