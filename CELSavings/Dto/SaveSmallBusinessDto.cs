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
        public string Name { get; set; }

        [Range(1, Int32.MaxValue,ErrorMessage ="Value must be greater than zero")]
        public decimal Amount { get; set; }
    }
}