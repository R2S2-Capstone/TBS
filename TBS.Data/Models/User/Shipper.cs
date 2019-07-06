﻿using System.ComponentModel.DataAnnotations;
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

        public string RIN { get; set; }

        public string DealerNumber { get; set; }
    }
}
