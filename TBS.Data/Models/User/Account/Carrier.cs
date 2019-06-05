using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.User.Account.Information;

namespace TBS.Data.Models.User.Account
{
    public class Carrier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User firebase ID required")]
        public string UserFirebaseId { get; set; }

        [Required(ErrorMessage = "Company information required")]
        public Company Company { get; set; }

        public string RIN { get; set; }

        public string DealerNumber { get; set; }

        // Payment information
    }
}
