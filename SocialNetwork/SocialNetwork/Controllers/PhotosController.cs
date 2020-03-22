using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class PhotosController : Controller
    {
        private readonly PhotoSocialNetwork_DB db = new PhotoSocialNetwork_DB();

        // GET: Photos
        public ActionResult Index()
        {
            return View("Index", db.Photos.ToList());
        }

        //GET: Render _PhotoGallery Partial View to display photo gallery in Home/Index page
        [ChildActionOnly]
        public ActionResult PhotoGallery(int num = 0)
        {
            //In the view, display the latest photos when num is greater than 0
            //Otherwise, display all photos
            List<Photo> photos;

            if (num == 0)
            {
                photos = db.Photos.ToList();
            }
            else
            {
                photos = db.Photos
                    .OrderByDescending(p => p.createdDate)
                    .Take(num).ToList();
            }

            return PartialView("~/Views/Shared/_PhotoGallery.cshtml", photos);
        }

        //Grabs the photo file to its associated photo ID
        public ActionResult GetImage(int photoId)
        {
            Photo requestedPhoto = db.Photos.FirstOrDefault(p => p.photoId == photoId);

            if (requestedPhoto != null)
            {
                return File(requestedPhoto.photoFilePath, requestedPhoto.imageMimeType);
            }
            else
            {
                return null;
            }
        }

        // GET: Photos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = db.Photos.Find(id);

            if (photo == null)
            {
                return HttpNotFound();
            }

            return View("Details", photo);
        }

        // GET: Photos/Create
        public ActionResult Create()
        {
            if (Session["username"] != null)
            {
                Photo newPhoto = new Photo
                {
                    username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Session["username"].ToString()),
                    createdDate = DateTime.Today,
                    modifiedDate = DateTime.Today
                };

                return View(newPhoto);
            }
            else
            {
                TempData["LoginMessage"] = "You must be logged in to do that.";

                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Photos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Session["username"].ToString());
            photo.createdDate = DateTime.Today;
            photo.modifiedDate = DateTime.Today;

            if (ModelState.IsValid)
            {
                //Read photo content type and file size, then save the photo
                if (image != null)
                {
                    photo.imageMimeType = image.ContentType;

                    string filePath = Path.Combine(Server.MapPath("~/Images/"), image.FileName);
                    photo.photoFilePath = filePath;

                    photo.photoImage.SaveAs(filePath);
                    
                    //Need to save filepath and image.mimeType to database
                    //Consider rebuilding database
                }

                db.Photos.Add(photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photo);
        }

        // GET: Photos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "photoId,title,photoFile,imageMimeType,description,username,createdDate,modifiedDate")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: Photos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = db.Photos.Find(id);

            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: Photos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
