using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class AccountViewModel
    {
        public LoginViewModel login { get; set; }
        public RegisterViewModel register { get; set; }
        public ResetPasswordViewModel resetPassword { get; set; }
    }
}