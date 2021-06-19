using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; set; } 
            = Guid.NewGuid().ToString();

        [Required]
        [MinLength(DataConstants.DefaultMinLength)]
        [MaxLength(DataConstants.DefaultMaxLength)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(DataConstants.DefaultMinLength)]
        [MaxLength(DataConstants.DefaultMaxLength)]
        public string Password { get; set; }

        public bool IsMechanic { get; set; }
    }
}
