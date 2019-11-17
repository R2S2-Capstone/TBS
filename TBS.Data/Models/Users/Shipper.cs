using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.Posts.Shipper;
using TBS.Data.Models.Reviews;
using TBS.Data.Models.Users.Information;

namespace TBS.Data.Models.Users
{
    // Dealer
    public class Shipper
    {
        public Guid Id { get; set; }

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

        public virtual IEnumerable<ShipperReview> Reviews { get; set; }
    }
}
