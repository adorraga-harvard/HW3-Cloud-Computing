using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Threading.Tasks;
using Voice.DataObjects;
using System.Web.Http.Controllers;
using System.Web.Http.OData;


namespace Voice.Controllers
{
    public class profileController : ApiController
    {
        public ApiServices Services { get; set; }
        Models.MobileServiceContext context = new Models.MobileServiceContext();
       
        /// <summary>
        /// For a user to be able to view his own Profile Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<Pattern.profileList>    Get(string id)
        {
            Services.Log.Info("Hello from profile controller!");
            var result = from p in context.Users
                         where p.Id == id
                         select new Pattern.profileList
                         {
                             name = p.Name,
                             gender = p.Gender,
                             profilePictureURL = p.PictureURL,
                             registrationStatus = p.RegistrationStatus,
                             voteBalance = p.VoteBalance,
                         };
            return result;
        }

        /// <summary>
        /// Update user info only when status is Approved or Pending
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<User> PatchUserAsync(string id, Delta<User> patch)
        {
            var c = patch.GetEntity();
            // find the user
            DataObjects.User user = (from u in context.Users
                                     where u.Id == id
                                     select u).SingleOrDefault();
            if (user != default(DataObjects.User)) /// If the user is found
            {
                if(user.RegistrationStatus == "Approved"  || user.RegistrationStatus == "Pending")
                {
                    if(c.Name!=user.Name) user.Name = c.Name;
                    if(c.PictureURL  != user.PictureURL ) user.PictureURL = c.PictureURL;
                    if(c.Gender != user.Gender) user.Gender = c.Gender;
                    await context.SaveChangesAsync(); 
                }
            }
            return user;
           }

    }
}
