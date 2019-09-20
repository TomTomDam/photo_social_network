﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class PhotosController : Controller
    {
        private PhotoSocialNetwork_DB db = new PhotoSocialNetwork_DB();

        // GET: Photos
        public ActionResult Index()
        {
            return View("Index", db.Photos.ToList());
        }

        //GET: Photos/PhotoGallery
        //Render a partial view to display photo gallery in Home/Index page
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

            return PartialView("PhotoGallery", photos);
        }

        //Grabs the photo file to its associated photo ID
        public FileContentResult GetImage(int photoId)
        {
            Photo requestedPhoto = db.Photos.FirstOrDefault(p => p.photoId == photoId);

            if (requestedPhoto != null)
            {
                return File(requestedPhoto.photoFile, requestedPhoto.imageMimeType);
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
            Photo photo = new Photo
            {
                username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Session["username"].ToString()),
                createdDate = DateTime.Today,
                modifiedDate = DateTime.Today
            };

            return View();
        }

        // POST: Photos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "photoId,title,photoFile,imageMimeType,description,username,createdDate,modifiedDate")] Photo photo, HttpPostedFileBase image)
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
                    photo.photoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.photoFile, 0, image.ContentLength);
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}