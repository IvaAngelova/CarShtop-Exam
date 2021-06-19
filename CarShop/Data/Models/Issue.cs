using System;
using System.ComponentModel.DataAnnotations;

namespace CarShop.Data.Models
{
    public class Issue
    {
        [Key]
        [Required]
        public string Id { get; set; }
         = Guid.NewGuid().ToString();

        [Required]
        [MinLength(DataConstants.DescriptionMinLength)]
        public string Description { get; set; }

        public bool IsFixed { get; set; }

        [Required]
        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}
