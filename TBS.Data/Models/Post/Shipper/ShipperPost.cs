using System;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.General;
using TBS.Data.Models.User.Information;
using TBS.Data.Models.Vehicle;

namespace TBS.Data.Models.Post.Shipper
{
    // Dealer post
    public class ShipperPost
    {
        public int Id { get; set; }

        public TBS.Data.Models.User.Shipper Shipper { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Vehicle required")]
        public PostedVehicle Vehicle { get; set; }

        [Required(ErrorMessage = "Pickup location required")]
        public Address PickupLocation { get; set; }

        [Required(ErrorMessage = "Pickup date required")]
        public DateTime PickupDate { get; set; }

        [Required(ErrorMessage = "Pickup contact required")]
        public Contact PickupContact { get; set; }

        [Required(ErrorMessage = "Dropoff location required")]
        public Address DropoffLocation { get; set; }

        [Required(ErrorMessage = "Dropoff date required")]
        public DateTime DropoffDate { get; set; }

        [Required(ErrorMessage = "Dropoff contact required")]
        public Contact DropoffContact { get; set; }

        [Required(ErrorMessage = "Starting bid required")]
        [Range(0, double.MaxValue, ErrorMessage = "Must be a valid starting bid")]
        public decimal StartingBid { get; set; }

        public PostStatus PostStatus { get; set; } = PostStatus.Open;
    }
}
