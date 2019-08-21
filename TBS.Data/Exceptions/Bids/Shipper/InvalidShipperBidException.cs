using System;

namespace TBS.Data.Exceptions.Bids.Shipper
{
    public class InvalidShipperBidException : Exception
    {
        public InvalidShipperBidException() : base("Invalid shipper bid")
        {

        }

        public InvalidShipperBidException(string message) : base(message)
        {

        }
    }
}
