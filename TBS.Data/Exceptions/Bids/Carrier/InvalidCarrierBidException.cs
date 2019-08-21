using System;

namespace TBS.Data.Exceptions.Bids.Carrier
{
    public class InvalidCarrierBidException : Exception
    {
        public InvalidCarrierBidException() : base("Invalid carrier bid")
        {

        }

        public InvalidCarrierBidException(string message) : base(message)
        {

        }
    }
}
