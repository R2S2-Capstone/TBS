using System;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.General;
using TBS.Data.Models.Posts.Carrier;
using TBS.Data.Models.Vehicle;

namespace TBS.Data.Models.Bids.Carrier
{
    // Note: this is a bid placed on a carriers post by a shipper
    public class CarrierBid
    {
        public int Id { get; set; }

        public Users.Shipper Shipper { get; set; }

        public CarrierPost Post { get; set; }

        [Required(ErrorMessage = "Vehicle required")]
        public PostedVehicle Vehicle { get; set; }

        [Required(ErrorMessage = "Pickup location required")]
        public Address PickupLocation { get; set; }

        [Required(ErrorMessage = "Pickup date required")]
        public DateTime PickupDate { get; set; }

        [Required(ErrorMessage = "Dropoff location required")]
        public Address DropoffLocation { get; set; }

        [Required(ErrorMessage = "Dropoff date required")]
        public DateTime DropoffDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double BidAmount { get; set; }

        public DateTime DateBidPlaced { get; set; } = DateTime.Now;

        public BidStatus BidStatus { get; set; } = BidStatus.Open;
    }
}
