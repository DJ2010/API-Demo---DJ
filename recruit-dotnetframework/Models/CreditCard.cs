using recruit_dotnetframework.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace recruit_dotnetframework.Models
{
    public class CreditCard
    {
        [Required(ErrorMessage = "Card number is required.")]
        [CreditCard(ErrorMessage = "Card number format is invalid.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiry date is required.")]
        [FutureDate(ErrorMessage = "Card has expired.")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "CVC is required.")]
        [Range(100, 9999, ErrorMessage = "CVC must be between 100 and 9999.")]
        public int CVC  { get; set; }
    }
}