using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class RegisterViewModel
    {
        [Key]
        [System.Web.Mvc.HiddenInput]
        public int id { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required]
        [Display(Name = "Email")]
        //[RegularExpression("",ErrorMessage = "This email is not valid.")]
        public string email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmPassword { get; set; }
    }
}