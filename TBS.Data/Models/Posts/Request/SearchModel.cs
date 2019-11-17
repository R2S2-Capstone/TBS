using System;

namespace TBS.Data.Models.Posts.Request
{
    public class SearchModel
    {
        public DateTime DropoffDate { get; set; } = DateTime.Parse("10/10/1000");

        public DateTime PickupDate { get; set; } = DateTime.Parse("10/10/1000");

        public string DropoffCity { get; set; } = null;

        public string PickupCity { get; set; } = null;
    }
}