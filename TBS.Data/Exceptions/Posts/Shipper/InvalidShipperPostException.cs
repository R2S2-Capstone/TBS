using System;

namespace TBS.Data.Exceptions.Posts.Shipper
{
    public class InvalidShipperPostException : Exception
    {
        public InvalidShipperPostException() : base("Invalid shipper post")
        {

        }

        public InvalidShipperPostException(string message) : base(message)
        {

        }
    }
}
