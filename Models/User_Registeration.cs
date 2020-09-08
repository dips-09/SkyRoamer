using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sky_Roamer.Models
{
    public class User_Registeration
    {
        [Key]
        [Display(Name = "User Name")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string First_name { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string Last_name { get; set; }
        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? Date_of_Birth { get; set; }
        [Required]
        [Display(Name = "Gender")]
        public Gender UserGender { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        
        [Required]
        public string Password { get; set; }
        [NotMapped]
        [Display(Name ="Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name ="Category")]
        public Category UserCategory { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Category
    {
        Customer,
        Travel_Agent
    }
}