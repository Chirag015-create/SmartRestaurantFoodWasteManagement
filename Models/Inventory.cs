using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRestaurantWasteManagement.Models
{
    [Table("Inventories")]
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "Item name is required")]
        [StringLength(150)]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; } // Vegetables, Meat, Dairy, Grains, Spices

        [Required]
        [Display(Name = "Current Stock")]
        [Range(0, 100000)]
        public double CurrentStock { get; set; }

        [Required]
        [StringLength(50)]
        public string Unit { get; set; } // Kg, Liters, Units, Bags

        [Display(Name = "Minimum Threshold")]
        [Range(0, 10000)]
        public double MinimumThreshold { get; set; }

        [Display(Name = "Unit Cost ($)")]
        [DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }
}
