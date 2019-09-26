using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork.Models
{
    public class Photo
    {
        [Key]
        [HiddenInput]
        public int photoId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string title { get; set; }

        [Display(Name = "Picture")]
        [MaxLength]
        public string photoFilePath { get; set; }

        [Display(Name = "Upload Image")]
        [MaxLength]
        [NotMapped]
        public HttpPostedFileBase photoImage { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Image MIME Type")]
        public string imageMimeType { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Username")]
        public string username { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        public DateTime createdDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Modified Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yy}", ApplyFormatInEditMode = true)]
        public DateTime modifiedDate { get; set; }

        public virtual ICollection<Comment> comments { get; set; }
    }
}