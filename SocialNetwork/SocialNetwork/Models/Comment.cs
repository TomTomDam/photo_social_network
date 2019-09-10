using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class Comment
    {
        public int commentId { get; set; }
        public int photoId { get; set; }
        public string username { get; set; }

        [DataType(DataType.MultilineText)]
        public string text { get; set; }
        
        public virtual Photo photo { get; set; }
    }
}