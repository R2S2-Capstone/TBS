using System;

namespace TBS.Data.Exceptions.Bids
{
    public class FailedToUpdateBidException : Exception
    {
        public FailedToUpdateBidException() : base("Failed to update bid")
        {

        }

        public FailedToUpdateBidException(string message) : base(message)
        {

        }
    }
}
