using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBS.Data.Models.Reviews
{
    public class CarrierReviews
    {
        public Guid ID { get; set; } 
        public Users.Carrier Carrier { get; set; }
        [Required(ErrorMessage = "Rating required")]
        public String reviewer { get; set; }
        public int rating { get; set; }
        public string review { get; set; }
        public DateTime date { get; set; }
    }
}
