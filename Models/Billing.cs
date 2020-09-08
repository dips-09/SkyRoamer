using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sky_Roamer.Models
{
    public class Billing
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillId { get; set; }

        [ForeignKey("Book_Package")]
        public int BookingId { get; set; }

        [Required]
        public DateTime Bill_Date { get; set; }

        [Required]
        public int total_Amt { get; set; }

        [Required]
        public string Mode_Of_Payment { get; set; }

        public virtual Book_Package Book_Package { get; set; }
    }
}