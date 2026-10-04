using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRestaurantWasteManagement.Models
{
    [Table("DailyFoods")]
    public class DailyFood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DailyFoodId { get; set; }

        [Required]
        public int FoodId { get; set; }

        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }

        [Required]
        [Display(Name = "Quantity Prepared")]
        [Range(0, 10000, ErrorMessage = "Quantity prepared must be positive")]
        public double QuantityPrepared { get; set; }

        [Display(Name = "Quantity Sold")]
        [Range(0, 10000)]
        public double QuantitySold { get; set; }

        [Display(Name = "Quantity Left")]
        [Range(0, 10000)]
        public double QuantityLeft { get; set; }

        [Required]
        [Display(Name = "Preparation Date")]
        [DataType(DataType.Date)]
        public DateTime PrepDate { get; set; } = DateTime.Today;

        [Display(Name = "Expiry Time")]
        public DateTime? ExpiryTime { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }
    }
}
