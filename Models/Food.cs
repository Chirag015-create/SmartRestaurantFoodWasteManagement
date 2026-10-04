using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRestaurantWasteManagement.Models
{
    [Table("Foods")]
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodId { get; set; }

        [Required(ErrorMessage = "Food item name is required")]
        [StringLength(150)]
        [Display(Name = "Food Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(100)]
        public string Category { get; set; } // Main Course, Appetizer, Dessert, Beverage, Ingredient

        [Required]
        [Range(0.01, 100000.00, ErrorMessage = "Price must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string Unit { get; set; } // Kg, Portion, Plate, Liter, Gram

        [Display(Name = "Shelf Life (Days)")]
        [Range(1, 365, ErrorMessage = "Shelf life must be between 1 and 365 days")]
        public int ShelfLifeDays { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
