﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.Post.Shipper;
using TBS.Data.Models.User.Information;

namespace TBS.Data.Models.User
{
    // Dealer
    public class Shipper
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "User firebase ID required")]
        public string UserFirebaseId { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company information required")]
        public Company Company { get; set; }

        [Required(ErrorMessage = "RIN required")]
        public string RIN { get; set; }

        [Required(ErrorMessage = "Dealer number required")]
        public string DealerNumber { get; set; }

        public virtual IEnumerable<ShipperPost> Posts { get; set; }
    }
}
