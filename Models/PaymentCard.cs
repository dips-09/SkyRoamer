using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sky_Roamer.Models
{
    public class PaymentCard
    {
        [Required(ErrorMessage ="Enter Card Number")]
        public int CardNumber { get; set; }

        [Required(ErrorMessage = "Enter Expiry Date")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        [Required(ErrorMessage = "Enter CVV")]
        public int Cvv { get; set; }
    }
}