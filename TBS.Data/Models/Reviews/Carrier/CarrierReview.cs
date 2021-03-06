﻿using System;
using TBS.Data.Models.Users;

namespace TBS.Data.Models.Reviews
{
    public class CarrierReview
    {
        public Guid ID { get; set; } 

        public Carrier Carrier { get; set; }

        public Shipper Shipper { get; set; }

        public double Rating { get; set; }

        public string Review { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
