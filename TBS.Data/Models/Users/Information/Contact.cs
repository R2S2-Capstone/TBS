using System.ComponentModel.DataAnnotations;

namespace TBS.Data.Models.Users.Information
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [Phone(ErrorMessage = "Please use the format of 111-1111-1111")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
    }
}
