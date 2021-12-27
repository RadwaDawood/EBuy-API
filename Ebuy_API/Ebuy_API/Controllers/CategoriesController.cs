using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using Ebuy_API.Models;


namespace Ebuy_API.Controllers
{
    public class CategoriesController : ApiController
    {
        private EbuyContext db = new EbuyContext();

        // GET: api/Categories
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }
        //public IQueryable<Product> GetCategoriesById(int id)
        //{
        //    return db.Products.Where(c => c.cat_id == id);
        //}

        // GET: api/Categories/5
        //[ResponseType(typeof(Category))]
        //public IHttpActionResult GetCategory(int id)
        //{
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(category);
        //}

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        //[Authorize]
        public IHttpActionResult PutCategory(int id, Category category)
        {
            //List<Claim> st = (User.Identity as ClaimsIdentity).Claims.ToList();
            //if (st[2].Value == "admin")
            //{
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != category.cat_id)
                {
                    return BadRequest();
                }

                db.Entry(category).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return StatusCode(HttpStatusCode.NoContent);
            //}
            //else return Unauthorized();
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        
       // [Authorize]
        public IHttpActionResult PostCategory(Category category)
        {
            //List<Claim> st = (User.Identity as ClaimsIdentity).Claims.ToList();
            //if (st[2].Value == "admin")
            //{
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}

                db.Categories.Add(category);
                db.SaveChanges();

                return Ok(category);
               // return CreatedAtRoute("DefaultApi", new { id = category.cat_id }, category);
            //}
            //else return Unauthorized();
        }

        // DELETE: api/Categories/5
       // [ResponseType(typeof(Category))]
        //[Route("api/delete_category/{id}")]
        //[Authorize]

        public IHttpActionResult DeleteCategory(int id)
        {
            //List<Claim> st = (User.Identity as ClaimsIdentity).Claims.ToList();
            //if (st[2].Value == "admin")
            //{
                Category category = db.Categories.Find(id);
                if (category == null)
                {
                    return NotFound();
                }

                db.Categories.Remove(category);
                db.SaveChanges();

                return Ok(category);
            //}
            //else return Unauthorized();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.cat_id == id) > 0;
        }
    }
}