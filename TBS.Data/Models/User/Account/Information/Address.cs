using System.ComponentModel.DataAnnotations;

namespace TBS.Data.Models.User
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Address required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City name required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Provincial code must only be two characters long")]
        public string ProvinceCode { get; set; }

        [Required(ErrorMessage = "City name required")]
        public string County { get; set; }
        [Required(ErrorMessage = "Postal code required")]
        public string PostalCode { get; set; }
    }
}
