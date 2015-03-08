using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

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

    }
}
