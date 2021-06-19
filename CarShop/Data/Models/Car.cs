using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Car
    {
        [Key]
        [Required]
        public string Id { get; set; }
         = Guid.NewGuid().ToString();

        [Required]
        [MinLength(DataConstants.DefaultMinLength)]
        [MaxLength(DataConstants.DefaultMaxLength)]
        public string Model { get; set; }

        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        [MaxLength(DataConstants.PlateNumberMaxLength)]
        public string PlateNumber { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public IEnumerable<Issue> Issue { get; set; }
            = new List<Issue>();
    }
}
