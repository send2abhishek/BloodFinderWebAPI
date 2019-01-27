using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BloodTraceWebApi.Helpers;
using BloodTraceWebApi.Models;

namespace BloodTraceWebApi.Controllers
{
    public class BloodUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public IHttpActionResult Post(BloodUser bloodUser)
        {

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }
            //now we have to handle Images

            var stream = new MemoryStream(bloodUser.ImageArray);

            //Guid used for uniquely identify the resource
            var guid = Guid.NewGuid().ToString();
            var file = string.Format("{0}.jpg", guid);
            var folder = "~/Content/Users";
            var fullPath = string.Format("{0}/{1}", folder, file);

            var response = FilesHelper.UploadPhoto(stream, folder, file);

            if (response)
            {
                bloodUser.Imagepath = fullPath;
            }

            var user = new BloodUser()
            {
               UserName = bloodUser.UserName,
                BloodGroup = bloodUser.BloodGroup,
                Email=bloodUser.Email,
                Phone=bloodUser.Phone,
                Country=bloodUser.Country,
                Date=bloodUser.Date,
                Imagepath=bloodUser.Imagepath
            };

            db.BloodUsers.Add(user);
            db.SaveChanges();
            return StatusCode(HttpStatusCode.Created);
        }

        public IEnumerable<BloodUser> Get(string bloodGroup,string country)
        {


            var bloodUsers=db.BloodUsers.Where(user => user.BloodGroup.StartsWith(bloodGroup) && user.Country.StartsWith(country));
            return bloodUsers;
        }

        public IEnumerable<BloodUser> Get()
        {

            //returns the latest USers
            var latesUsers = db.BloodUsers.OrderByDescending(user => user.Date);
            return latesUsers;
        }


    }
}