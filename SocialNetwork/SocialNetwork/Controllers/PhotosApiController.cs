using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class PhotosApiController : ApiController
    {
        private PhotoSocialNetwork_DB db = new PhotoSocialNetwork_DB();

        public PhotosApiController()
        {
            db.Configuration.ProxyCreationEnabled = false; //Prevents the serialization of JSON responses
        }

        //GET: api/PhotosApi
        [HttpGet]
        public IEnumerable<Photo> GetAllPhotos()
        {
            return db.Photos.ToList();
        }

        //GET api/PhotosApi/{id}
        [HttpGet]
        [ResponseType(typeof(Photo))]
        public Photo GetPhotoById(int id)
        {
            Photo requestedPhoto = db.Photos.FirstOrDefault(p => p.photoId == id) as Photo;

            if (requestedPhoto == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return requestedPhoto;
        }

        //GET: api/PhotosApi/{username}
        [HttpGet]
        public IEnumerable<Photo> GetPhotoByUsername(string username)
        {
            List<Photo> userPhotos = db.Photos.Where(p => p.username == username).ToList();

            if (userPhotos.Count == 0)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            return userPhotos;
        }

        // PUT: api/PhotosApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPhoto(int id, Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != photo.photoId)
            {
                return BadRequest();
            }

            db.Entry(photo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PhotosApi
        [ResponseType(typeof(Photo))]
        public IHttpActionResult PostPhoto(Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Photos.Add(photo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = photo.photoId }, photo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoExists(int id)
        {
            return db.Photos.Count(e => e.photoId == id) > 0;
        }
    }
}