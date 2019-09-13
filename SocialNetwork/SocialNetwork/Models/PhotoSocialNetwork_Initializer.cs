using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class PhotoSocialNetwork_Initializer : DropCreateDatabaseAlways<PhotoSocialNetwork_DB>
    {
        protected override void Seed(PhotoSocialNetwork_DB context)
        {
            var photos = new List<Photo>
            {
                new Photo
                {

                },
                new Photo
                {

                },
                new Photo
                {

                }
            };
            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment
                {

                },
                new Comment
                {

                },
                new Comment
                {

                }
            };
            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();
        }

        private byte[] getFileBytes(string path)
        {
            FileStream file = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(file))
            {
                fileBytes = br.ReadBytes((int)file.Length);
            }

            return fileBytes;
        }
    }
}