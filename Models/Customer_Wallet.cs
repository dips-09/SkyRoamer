using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sky_Roamer.Models
{
    public class Customer_Wallet
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name ="Wallet Id")]
        public int WalletId{ get; set; }

        [Display(Name = "Customer Id")]        
        public string CustomerId { get; set; }

        [Display(Name = "Balance")]
        public int Balance { get; set; }
    }
}