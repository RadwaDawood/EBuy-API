using Ebuy_API.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace Ebuy_API.Controllers
{
    public class Log
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class OperationsController : ApiController
    {
        private EbuyContext db = new EbuyContext();

        public IHttpActionResult postlogin(Log log)
        {
            Admin admin = db.Admins.Where(n => n.admin_name == log.email && n.password == log.password).FirstOrDefault();
            User user = db.Users.Where(n=>n.email == log.email && n.password == log.password).FirstOrDefault();
            if(admin == null && user == null)
            {
                return Ok("not found");
            }
            string key = "ebuygroup-hugeverybigkey";  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>();
            if(admin != null)
            {
                claims.Add(new Claim("id", admin.admin_id.ToString()));
                claims.Add(new Claim("name", admin.admin_name));
                claims.Add(new Claim("role", "admin"));
            }
            if (user != null)
            {
                claims.Add(new Claim("id", user.user_id.ToString()));
                claims.Add(new Claim("name", user.user_name));
                claims.Add(new Claim("role", "user"));
            }
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: credentials
            );
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { name = claims[1].Value.ToString(), role = claims[2].Value.ToString(), token = jwt_token });

        }
        // [Authorize]
        //public IHttpActionResult post()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        List<Claim> st = (User.Identity as ClaimsIdentity).Claims.ToList();


        //        return Ok(st[0].Value);
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}
    }
}
