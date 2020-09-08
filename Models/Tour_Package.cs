using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sky_Roamer.Models
{
    public class Tour_Package
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageId { get; set; }

        [ForeignKey("User_Registeration")]
        public string Travel_Agent_Id { get; set; }

        public virtual User_Registeration User_Registeration { get; set; }

        [Required]
        public string Package_Name { get; set; }

        [Required]
        public string Source { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string Per_head_cost { get; set; }

        public Byte[] images { get; set; }

    }
}