using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.User.Information;

namespace TBS.Data.Models.User
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

        public string DriversLicense { get; set; }
    }
}
