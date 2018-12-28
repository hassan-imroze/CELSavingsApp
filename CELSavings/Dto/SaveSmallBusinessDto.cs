using CELSavings.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CELSavings.Dto
{
    public class SaveSmallBusinessDto
    {
        [Required]
        public string Product { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public int CustomerOrGuarantorId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        [Range(1, double.MaxValue,ErrorMessage ="{0} must be greater than zero")]
        [LessThanTotalBalance]
        [Display(Name = "Buying Price")]
        public double BuyingPrice { get; set; }

        [Range(1, 20, ErrorMessage = "{0} must be between 1 and 20")]
        [Display(Name = "Profit percentage")]
        public double ProfitPercentage { get; set; }

        [DoubleGreaterThan(PropertyNameToCheck: nameof(BuyingPrice),PropertyNametoCheckDispaly:"Buying Price",  ErrorMessage = "{0} must be greater than {1}")]
        [Display(Name = "Selling Price")]
        public double SellingPrice { get; set; }

        [DateRequired(ErrorMessage ="A valid '{0}' is required")]
        [Display(Name ="Sell Date")]
        public DateTime? SellDate { get; set; }

        
        [DoubleLessThanEqual(PropertyNameToCheck: nameof(SellingPrice),PropertyNametoCheckDispaly:"Selling Price", ErrorMessage = "'{0}' cannot be greater than '{1}'")]
        [Display(Name = "Initial Payment")]
        public double InitialPayment { get; set; }

        [DateRequired(ErrorMessage = "A valid '{0}' is required")]
        [DateGreaterThanOrEqual(PropertyNameToCheck: nameof(SellDate), PropertyNametoCheckDispaly: "Sell Date", ErrorMessage = "'{0}' must be greater than or equal '{1}'")]
        [Display(Name = "Installment Start Date")]
        public DateTime? InstallmentStartDate { get; set; }

        
    }
}