using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class PhotoSocialNetwork_Initializer : DropCreateDatabaseIfModelChanges<PhotoSocialNetwork_DB>
    {
        public override void InitializeDatabase(PhotoSocialNetwork_DB context)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
            , string.Format("ALTER DATABASE {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));


            base.InitializeDatabase(context);
        }

        protected override void Seed(PhotoSocialNetwork_DB context)
        {
            base.Seed(context);

            var photos = new List<Photo>
            {
                new Photo
                {
                    title = "Dog",
                    description = "Look how excited Buddy gets when I tell him we are going for a walk!",
                    username = "John Shepard",
                    photoFilePath = "\\Images\\excited_doggo.gif",
                    imageMimeType = "image/gif",
                    createdDate = DateTime.Today,
                    modifiedDate = DateTime.Today,
                },
                new Photo
                {
                    title = "Flower",
                    description = "The Lily of the Valley, a flower that represents happiness.",
                    username = "Jane Doe",
                    photoFilePath = "\\Images\\lilyofthevalley.jpg",
                    imageMimeType = "image/jpeg",
                    createdDate = DateTime.Today,
                    modifiedDate = DateTime.Today,
                },
                new Photo
                {
                    title = "River",
                    description = "Would love to visit the Yangtze River someday!",
                    username = "John Smith",
                    photoFilePath = "\\Images\\yangtze_river.jpg",
                    imageMimeType = "image/jpeg",
                    createdDate = DateTime.Today,
                    modifiedDate = DateTime.Today,
                }
            };
            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment
                {
                    photoId = 1,
                    username = "Thomas Dam",
                    text = "My reaction when I get pizza for dinner.",
                },
                new Comment
                {
                    photoId = 1,
                    username = "Geralt Rivia",
                    text = "What a happy pupper!",
                },
                new Comment
                {
                    photoId = 2,
                    username = "Mr A. Anderson",
                    text = "Great picture Jane!",
                },
                new Comment
                {
                    photoId = 3,
                    username = "Rick Morty",
                    text = "Me too!",
                }
            };
            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();
        }
    }
}