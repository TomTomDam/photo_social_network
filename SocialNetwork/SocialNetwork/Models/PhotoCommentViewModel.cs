using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class PhotoCommentViewModel
    {
        public Photo photo { get; set; }
        public Comment comment { get; set; }
    }
}