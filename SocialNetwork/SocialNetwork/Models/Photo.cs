using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Models
{
    public class Photo
    {
        public int photoId { get; set; }

        [Required]
        public string title { get; set; }

        [Display(Name = "Picture")]
        [MaxLength]
        public string photoFilePath { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string imageMimeType { get; set; }

        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        public string username { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime createdDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Modified Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        public DateTime modifiedDate { get; set; }

        public virtual ICollection<Comment> comments { get; set; }
    }
}