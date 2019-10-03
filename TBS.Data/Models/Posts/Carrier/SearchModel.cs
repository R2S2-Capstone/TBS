using System;
using System.Collections.Generic;
using System.Text;

namespace TBS.Data.Models.Posts.Carrier
{
    public class SearchModel
    {
        public DateTime dropOffDate { get; set; } = DateTime.Parse("10/10/1000");

        public DateTime pickupDate { get; set; } = DateTime.Parse("10/10/1000");

        public string dropoffCity { get; set; } = null;

        public string pickupCity { get; set; } = null;

        public string dropoffProv { get; set; } = null;

        public string pickupProv { get; set; } = null;


    }
}