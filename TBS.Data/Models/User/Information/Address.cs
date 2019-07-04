using System.ComponentModel.DataAnnotations;

namespace TBS.Data.Models.User.Information
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Address required")]
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "City name required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Provincial code must only be two characters long")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Country required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postal code required")]
        public string PostalCode { get; set; }
    }
}
