using System.ComponentModel.DataAnnotations;

namespace TBS.Data.Models.User.Information
{
    public class Company
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Address required")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "Contact required")]
        public Contact Contact { get; set; }
    }
}
