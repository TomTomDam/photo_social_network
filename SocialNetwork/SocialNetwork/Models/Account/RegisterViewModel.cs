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

        [Required(ErrorMessage = "This field is required.")]
        [System.Web.Mvc.Remote("UsernameAlreadyExists", "Account", ErrorMessage = ("This {0} already exists. Please use a different {0}."))]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [System.Web.Mvc.Remote("EmailAlreadyExists", "Account", ErrorMessage = ("This {0} is already in use. Please use a different {0}."))]
        //[RegularExpression("",ErrorMessage = "This is not a valid email.")]
        [EmailAddress(ErrorMessage = "This is not a valid email.")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm password")]
        public string confirmPassword { get; set; }
    }

    [MetadataType(typeof(RegisterViewModel))]
    public partial class Register
    {

    }

    //public class UsernameEmailAlreadyExists : ValidationAttribute
    //{
    //    private AccountContext db = new AccountContext();

    //    public UsernameEmailAlreadyExists() : base("This {0} already exists. Please use a different {0}.")
    //    {

    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var username = db.user.Where(u => u.username == value.ToString()).FirstOrDefault();
    //        var email = db.user.Where(u => u.email == value.ToString()).FirstOrDefault();

    //        if (username == null)
    //        {
    //            return ValidationResult.Success;
    //        }
    //        else if (email == null)
    //        {
    //            return ValidationResult.Success;
    //        }
    //        else
    //        {
    //            return new ValidationResult("");
    //        }
    //    }
    //}
}