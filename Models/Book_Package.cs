using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sky_Roamer.Models
{
    public class Book_Package
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }

        [ForeignKey("Tour_Package")]
        public int PackageId { get; set; }
        public virtual User_Registeration User_Registeration { get; set; }

        [ForeignKey("User_Registeration")]
        public string CustomerId { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }

        [Display(Name = "Number of people")]
        public int Number_of_persons { get; set; }

        [Display(Name ="Date of Travelling")]
        [DataType(DataType.Date)]
        public DateTime? Date_of_Travelling { get; set; }

        [DefaultValue("Pending")]
        public string Status { get; set; }
        public virtual Tour_Package Tour_Package { get; set; }
    }
}