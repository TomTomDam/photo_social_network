using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Models
{
    public class Account
    {
        [Key]
        [HiddenInput]
        public int id { get; set; }

        public string username { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public bool rememberMe { get; set; }

        public string confirmPassword { get; set; }

        public string oldPassword { get; set; }

        public string newPassword { get; set; }

        public string confirmResetPassword { get; set; }
    }
}