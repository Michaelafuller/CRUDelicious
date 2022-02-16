using System;
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must be more than 3 characters")]
        [MaxLength(45, ErrorMessage = "Must be fewer than 45 characters")]
        [Display(Name = "Chef's Name")]
        public string Chef { get; set; }

        [Required]
        [Range(1,5, ErrorMessage = "Must be between 1 and 5")]
        public int Tastiness { get; set; }

        [Required]
        [Range(100, 1500, ErrorMessage = "Realistic calorie ranges: 100-1500")]
        [Display(Name = "# of Calories")]
        public int Calories { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "10 - 45 characters, please.")]
        [MaxLength(45, ErrorMessage = "10 - 45 characters, please.")]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}