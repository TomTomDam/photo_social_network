using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("ApplicationServices")
        {
        }
    }
}