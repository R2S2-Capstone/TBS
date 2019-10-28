using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBS.Data.Models.Reviews
{
    public class ShipperReview
    {
        public Guid ID { get; set; }
        public Users.Shipper shipper { get; set; }
        [Required(ErrorMessage = "Rating required")]
        public String reviewer { get; set; }
        public int rating { get; set; }
        public string review { get; set; }
        public DateTime date { get; set; }
    }
}
