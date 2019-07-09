using System;

namespace TBS.Data.Exceptions.Posts
{
    public class FailedToUpdatePostException : Exception
    {
        public FailedToUpdatePostException() : base("Failed to update post")
        {

        }

        public FailedToUpdatePostException(string message) : base(message)
        {

        }
    }
}
