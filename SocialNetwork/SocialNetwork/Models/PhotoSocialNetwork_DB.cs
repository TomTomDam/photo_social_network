using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class PhotoSocialNetwork_DB : DbContext
    {
        public PhotoSocialNetwork_DB() : base("PhotoSocialNetwork_DB")
        {
            Database.SetInitializer<PhotoSocialNetwork_DB>(new DropCreateDatabaseIfModelChanges<PhotoSocialNetwork_DB>());
        }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}