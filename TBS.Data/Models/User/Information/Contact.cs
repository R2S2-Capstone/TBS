using System.ComponentModel.DataAnnotations;

namespace TBS.Data.Models.User.Information
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Phone(ErrorMessage = "Please use the format of 111-1111-1111")]
        public string PhoneNumber { get; set; }
    }
}
