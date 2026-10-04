using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRestaurantWasteManagement.Models
{
    [Table("Wastes")]
    public class Waste
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WasteId { get; set; }

        [Required]
        public int FoodId { get; set; }

        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }

        [Required]
        [Display(Name = "Quantity Wasted")]
        [Range(0.01, 10000, ErrorMessage = "Wasted quantity must be greater than zero")]
        public double QuantityWasted { get; set; }

        [Required]
        [StringLength(50)]
        public string Unit { get; set; } // Kg, Portion, Grams, Liters

        [Required]
        [StringLength(200)]
        [Display(Name = "Reason for Waste")]
        public string Reason { get; set; } // Expired, Over-prepared, Spoiled, Customer Leftover

        [Display(Name = "Estimated Financial Loss ($)")]
        [DataType(DataType.Currency)]
        public decimal CostLoss { get; set; }

        [Display(Name = "Logged Date")]
        public DateTime LoggedDate { get; set; } = DateTime.Now;

        public int? LoggedByUserId { get; set; }

        [ForeignKey("LoggedByUserId")]
        public virtual User LoggedByUser { get; set; }
    }
}
