using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemoProject.Database;
using WebApiDemoProject.Models;

namespace WebApiDemoProject.Controllers
{
    public class UserController : ApiController
    {
        //[HttpGet]
        //public string Greet(string name)
        //{
        //    return "Welcome"+ name;
        //}
        DatabaseContext db = new DatabaseContext();
        //api/User
        public IEnumerable<User>GetUsers()
        {
            return db.Users.ToList();
        }
        //api/User/2
        public User GetUsers(int id)
        {
            return db.Users.Find(id);
        }
        //api/User
        [HttpPost]
        public HttpResponseMessage AddUser(User model)
        {
            try
            {
                db.Users.Add(model);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;


            }
            catch (Exception ex)
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateUser(int id,User model)
        {
            try
            {
                if(id==model.UserID)
                { 
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception ex)
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }
        //api/User
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            User user = db.Users.Find();
            if (user!=null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;

            }
            else
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }

        }
    }
}
