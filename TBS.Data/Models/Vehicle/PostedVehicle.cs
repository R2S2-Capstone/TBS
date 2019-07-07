﻿using System.ComponentModel.DataAnnotations;

namespace TBS.Data.Models.Vehicle
{
    public class PostedVehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vehicle year required")]
        [Range(1900, 2100, ErrorMessage = "Invalid vehicle year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Vehicle make required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Vehicle model required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "VIN required")]
        public string VIN { get; set; }
    }
}
