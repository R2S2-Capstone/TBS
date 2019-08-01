using System.ComponentModel.DataAnnotations;
using TBS.Data.Models.General;

namespace TBS.Data.Models.User.Information
{
    public class Company
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address required")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "Contact required")]
        public Contact Contact { get; set; }
    }
}
