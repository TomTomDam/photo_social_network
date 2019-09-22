using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using SocialNetwork.Models;
using System.Drawing;
using System.Web.Hosting;
using System.Reflection;

namespace SocialNetwork.Migrations.PhotoCommentContext
{
    internal sealed class Configuration : DbMigrationsConfiguration<SocialNetwork.Models.PhotoSocialNetwork_DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\PhotoCommentContext";
        }

        protected override void Seed(SocialNetwork.Models.PhotoSocialNetwork_DB context)
        {
            base.Seed(context);

            var photos = new List<Photo>
            {
                new Photo
                {
                    title = "Dog",
                    description = "Look how excited Buddy gets when I tell him we are going for a walk!",
                    username = "John Shepard",
                    photoFilePath = "excited_doggo.gif",
                    imageMimeType = "image/gif",
                    createdDate = DateTime.Today,
                    modifiedDate = DateTime.Today,
                },
                new Photo
                {
                    title = "Flower",
                    description = "The Lily of the Valley, a flower that represents happiness.",
                    username = "Jane Doe",
                    photoFilePath = "lilyofthevalley.jpg",
                    imageMimeType = "image/jpeg",
                    createdDate = DateTime.Today,
                    modifiedDate = DateTime.Today,
                },
                new Photo
                {
                    title = "River",
                    description = "Would love to visit the Yangtze River someday!",
                    username = "John Smith",
                    photoFilePath = "yangtze_river.jpg",
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

        //private byte[] getFileBytes(string path)
        //{
        //    FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
        //    byte[] fileBytes;
        //    using (BinaryReader br = new BinaryReader(fileOnDisk))
        //    {
        //        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
        //    }
        //    return fileBytes;
        //}
    }
}