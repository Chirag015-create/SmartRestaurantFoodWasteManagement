using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartRestaurantWasteManagement.Models
{
    [Table("Predictions")]
    public class Prediction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PredictionId { get; set; }

        [Required]
        public int FoodId { get; set; }

        [ForeignKey("FoodId")]
        public virtual Food Food { get; set; }

        [Required]
        [Display(Name = "Prediction Date")]
        [DataType(DataType.Date)]
        public DateTime PredictionDate { get; set; }

        [Display(Name = "Predicted Demand Quantity")]
        public double PredictedDemandQty { get; set; }

        [Display(Name = "Recommended Prep Quantity")]
        public double RecommendedPrepQty { get; set; }

        [Display(Name = "Estimated Waste Reduction (%)")]
        public double EstimatedWasteReductionPct { get; set; }

        [Display(Name = "Confidence Score")]
        public double ConfidenceScore { get; set; } // 0.0 to 1.0 (or percentage)

        [StringLength(100)]
        [Display(Name = "Prediction Model/Algorithm")]
        public string AlgorithmUsed { get; set; } // Exponential Smoothing, Linear Regression, Moving Average

        [Display(Name = "Generated At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
