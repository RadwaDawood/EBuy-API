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
using Ebuy_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.IO;

namespace Ebuy_API.Controllers
{
    public class ProductsController : ApiController
    {
        private EbuyContext db = new EbuyContext();

        // GET: api/Products
       // [Authorize]
        public IHttpActionResult GetProducts()
        {
            //List<Claim> st = (User.Identity as ClaimsIdentity).Claims.ToList();
            //if (st[2].Value == "user")
            //{
            //    return Ok(db.Products);
            //}
            //else if(st[2].Value == "admin")
            //{
                return Ok(db.Products);
            //}
            //else return Unauthorized();
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        // Get products by category ID
        [Route("api/Products/GetProductByCat/{id}")]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProductByCat(int id)
        {
            List<Product> products = db.Products.Where(n=>n.cat_id == id).ToList();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.product_id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        string imageUploaded;

        [HttpPost]
        [Route("api/Products/ImgUpload")]
        public string ImgUpload()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Attach"), fileName);
                file.SaveAs(path);
            }
            string img = file != null ? "/Attach/" + file.FileName : null;
            imageUploaded = img;
            return img;
        }
        // POST: api/Products
        [HttpPost]
        [ResponseType(typeof(Product))]
       // [Authorize]
        public IHttpActionResult PostProduct(Product product )
        {
            //List<Claim> st = (User.Identity as ClaimsIdentity).Claims.ToList();
            //if (st[2].Value == "admin")
            //{
                 
                if (!ModelState.IsValid)
                {               
                    return BadRequest(ModelState);
                }
        
            db.Products.Add(product);
                db.SaveChanges();
                return Created("DefaultApi",product);
               // return Ok(db.Products);
            //}
            //else return Unauthorized();

        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.product_id == id) > 0;
        }
    }
}