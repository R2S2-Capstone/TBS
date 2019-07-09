using System;

namespace TBS.Data.Exceptions.Posts.Carrier
{
    public class InvalidCarrierPostException : Exception
    {
        public InvalidCarrierPostException() : base("Invalid carrier post")
        {

        }

        public InvalidCarrierPostException(string message) : base(message)
        {

        }
    }
}
