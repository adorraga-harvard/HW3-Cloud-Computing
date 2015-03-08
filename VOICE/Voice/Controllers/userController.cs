using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Voice.DataObjects;
using Voice.Models;

namespace Voice.Controllers
{
    public class userController : TableController<User>
    {
        MobileServiceContext context = new MobileServiceContext();
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<User>(context, Request, Services);
        }
        

        public class Updater
        {
            public string ErrorCode { get; set; }
            public string Message { get; set; }
            public string RegistrationStatus { get; set; }

        }
        [HttpPatch]
        public Updater PatchUser(string id, Delta<User> patch)
        {
            var c = patch.GetEntity();
            int VoteBalance = 0;
            if (c.RegistrationStatus == "Approved") VoteBalance = 100;
            if (c.RegistrationStatus == "Declined") VoteBalance = 0;
            // find the current status of a given user
            DataObjects.User user = (from u in context.Users
                                     where u.Id == id
                                     select u).SingleOrDefault();
            if (user != default(DataObjects.User)) /// If the user is found
            {
                var curStatus = user.RegistrationStatus;
                if (curStatus == "Approved" || curStatus == "Declined")
                {
                    // HTTP Status Conflict               
                    var result = new Updater() { ErrorCode = "3", Message = "User's registration has already been set", RegistrationStatus = curStatus };
                    return result;
                }
                else
                {
                    // Update the  User Table with the Status and Role of the user
                    if (c.RegistrationStatus != null)
                    {
                        user.RegistrationStatus = c.RegistrationStatus;
                        user.VoteBalance = VoteBalance;
                    }
                    if (c.Role != null) user.Role = c.Role;
                    var result = new Updater() { ErrorCode = "1", Message = "User's registration successfully updated.", RegistrationStatus = c.RegistrationStatus };
                    // UpdateAsync(id, patch);
                    context.SaveChanges();
                    return result;

                }
            }
            else
            {
                var result = new Updater() { ErrorCode = "3", Message = "User Info not found", RegistrationStatus = "N/A" };
                return result;
            }
        }




        /// <summary>
        /// View a User with their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SingleResult<User> GetUser(string id)
        {
            return Lookup(id);
        }

        /// <summary>
        /// Deletes the user specified by their assigned ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteUser(string id)
        {
            return DeleteAsync(id);
        }

    }
}