using System;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.Vehicle.Carrier;

namespace TBS.Data.Models.Post.Carrier
{
    // Transporter post
    public class CarrierPost
    {
        public int Id { get; set; }

        public TBS.Data.Models.User.Carrier Carrier { get; set; }

        [Required(ErrorMessage = "Pickup city required")]
        public string PickupCity { get; set; }

        [Required(ErrorMessage = "Pickup date required")]
        public DateTime PickupDate { get; set; }

        [Required(ErrorMessage = "Dropoff city required")]
        public string DropoffCity { get; set; }

        [Required(ErrorMessage = "Dropoff date required")]
        public DateTime DropoffDate { get; set; }

        [Required(ErrorMessage = "Trailer type required")]
        public CarrierTrailerType TrailerType { get; set; }

        [Required(ErrorMessage = "Spaces available required")]
        public int SpacesAvailable { get; set; }

        [Required(ErrorMessage = "Cost required")]
        [Range(0, double.MaxValue, ErrorMessage = "Must be a valid cost")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Post status required")]
        public PostStatus PostStatus { get; set; }
    }
}
