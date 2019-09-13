using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("PhotoSocialNetwork_DB")
        {
            Database.SetInitializer<AccountContext>(new DropCreateDatabaseIfModelChanges<AccountContext>());
        }

        public DbSet<Account> user { get; set; }
/*        public DbSet<LoginModel> loginUser { get; set; }
        public DbSet<RegisterModel> registerUser { get; set; }*/
    }
}