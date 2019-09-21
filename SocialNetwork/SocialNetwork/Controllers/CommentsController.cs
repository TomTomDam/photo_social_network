using System;
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
    public class CommentsController : Controller
    {
        private PhotoSocialNetwork_DB db = new PhotoSocialNetwork_DB();

        //GET: Comments Partial View within the Photos/Details view
        [HttpGet]
        public PartialViewResult CommentPhoto(int photoId)
        {
            var comments = db.Comments.Where(c => c.photoId == photoId).ToList();

            ViewBag.PhotoId = photoId;

            return PartialView(comments);
        }

        //POST: Creates comment
        [HttpPost]
        public PartialViewResult CommentPhoto(Comment comment, int photoId)
        {
            comment.username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Session["username"].ToString());

            db.Comments.Add(comment);
            db.SaveChanges();

            var comments = db.Comments.Where(c => c.photoId == photoId).ToList();

            ViewBag.PhotoId = photoId;

            return PartialView("_CommentPhoto", comments);
        }

        // GET: Comments/Create
        public PartialViewResult Create(int _photoId)
        {
            //Create new comment
            Comment newComment = new Comment()
            {
                photoId = _photoId,
                username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Session["username"].ToString())
            };

            ViewBag.PhotoId = _photoId;

            return PartialView("_PostComment");
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commentId,photoId,username,text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.photoId = new SelectList(db.Photos, "photoId", "title", comment.photoId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.photoId = new SelectList(db.Photos, "photoId", "title", comment.photoId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentId,photoId,username,text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.photoId = new SelectList(db.Photos, "photoId", "title", comment.photoId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.Find(id);

            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Photo", new { id = comment.photoId });
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
