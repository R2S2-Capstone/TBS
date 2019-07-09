using System;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.General;
using TBS.Data.Models.Vehicle;

namespace TBS.Data.Models.Post.Shipper
{
    // Dealer post
    public class ShipperPost
    {
        public int Id { get; set; }

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

        [Required(ErrorMessage = "Cost required")]
        [Range(0, double.MaxValue, ErrorMessage = "Must be a valid cost")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Post status required")]
        public PostStatus PostStatus { get; set; }
    }
}
