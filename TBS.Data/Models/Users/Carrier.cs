using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.Users.Information;
using TBS.Data.Models.Vehicle.Carrier;

namespace TBS.Data.Models.Users
{
    // Transporter
    public class Carrier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User firebase ID required")]
        public string UserFirebaseId { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company information required")]
        public Company Company { get; set; }

        [Required(ErrorMessage = "Drivers license required")]
        public string DriversLicense { get; set; }

        public CarrierVehicle Vehicle { get; set; }
    }
}
