﻿using System;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.Vehicle.Carrier;

namespace TBS.Data.Models.Posts.Carrier
{
    // Transporter post
    public class CarrierPost
    {
        public int Id { get; set; }

        public int CarrierId { get; set; }
        public Users.Carrier Carrier { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Pickup location required")]
        public string PickupLocation { get; set; }

        [Required(ErrorMessage = "Pickup date required")]
        public DateTime PickupDate { get; set; }

        [Required(ErrorMessage = "Dropoff location required")]
        public string DropoffLocation { get; set; }

        [Required(ErrorMessage = "Dropoff date required")]
        public DateTime DropoffDate { get; set; }

        [Required(ErrorMessage = "Trailer type required")]
        public CarrierTrailerType TrailerType { get; set; }

        [Required(ErrorMessage = "Spaces available required")]
        public int SpacesAvailable { get; set; }

        [Required(ErrorMessage = "Cost required")]
        [Range(0, double.MaxValue, ErrorMessage = "Must be a valid cost")]
        public decimal StartingBid { get; set; }

        public PostStatus PostStatus { get; set; } = PostStatus.Open;
    }
}