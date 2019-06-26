using System.ComponentModel.DataAnnotations;

namespace TBS.Data.Models.User.Information
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [Phone(ErrorMessage = "Please use the format of 111-1111-1111")]
        public string PhoneNumber { get; set; }
    }
}
