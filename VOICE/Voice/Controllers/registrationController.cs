using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Voice.Controllers
{
    public class registrationController : ApiController
    {
        public ApiServices Services { get; set; }
        Models.MobileServiceContext context = new Models.MobileServiceContext();
        public class AllUsers
        {
            public string IDPId { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string PictureURL { get; set; }
            public string VoteBalance { get; set; }
            public string RegistrationStatus { get; set; }
            public string Role { get; set; }

            public static implicit operator SingleResult<object>(AllUsers v)
            {
                throw new NotImplementedException();
            }
        }
        public class AdminUsers
        {
            public string IDPId { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string PictureURL { get; set; }
            public string VoteBalance { get; set; }
            public string RegistrationStatus { get; set; }
            public string Role { get; set; }
        }

        public class AllUsersStatus
        {
            public string Id { get; set; }

            public string IDPId { get; set; }
            public string Name { get; set; }
            public string Gender { get; set; }
            public string PictureURL { get; set; }
            public int VoteBalance { get; set; }
            public string RegistrationStatus { get; set; }
            public string Role { get; set; }
        }

        public class NewRegistration
        {
            public string ErrorCode { get; set; }
            public string Message { get; set; }
            public string RegistrationStatus { get; set; }
        }


        // GET api/registration
        /// <summary>
        /// This is for GET info for all users
        /// </summary>
        /// <param name="IDPId">Coming from the ID Provider like Twitter, Google, etc...</param>
        /// <returns></returns>
        public Object GetOne(string IDPId)
        {
            Services.Log.Info("Hello from custom controller!");
            var result = (from u in context.Users
                          where u.IDPId == IDPId
                          select new
                          {
                              Name = u.Name,
                              Gender = u.Gender,
                              PictureURL = u.PictureURL,
                              RegistrationStatus = u.RegistrationStatus,
                              Role = u.Role,
                              VoteBalance = u.VoteBalance
                          }).SingleOrDefault()
                         ;

            return result;
        }

        /// <summary>
        /// This is for GET info for ADMIN users only
        /// Take note of the parameter name
        /// and it returns with IDPId together with what we have above
        /// </summary>
        /// <param name="IDPUserId"></param>
        /// <returns></returns>
        public Object GetAdmin(string IDPUserId)
        {
            Services.Log.Info("Hello from custom controller!");
            Models.MobileServiceContext context = new Models.MobileServiceContext();
            var result = (from u in context.Users
                          where u.IDPId == IDPUserId
                          select new
                          {
                              IDPId = u.IDPId,
                              Name = u.Name,
                              Gender = u.Gender,
                              PictureURL = u.PictureURL,
                              RegistrationStatus = u.RegistrationStatus,
                              Role = u.Role,
                              VoteBalance = u.VoteBalance
                          }).SingleOrDefault();
            return result;
        }


        /// <summary>
        /// This is for GET info with Status paraameter 
        /// </summary>
        /// <param name="RegistrationStatus"></param>
        /// <returns></returns>
        public IQueryable<AllUsersStatus> GetAllStatus(string RegistrationStatus)
        {
            Services.Log.Info("Hello from custom controller!");
            Models.MobileServiceContext context = new Models.MobileServiceContext();
            if (RegistrationStatus == "All")
            {
                var result = from u in context.Users
                             select new AllUsersStatus()
                             {
                                 Id = u.Id,
                                 IDPId = u.IDPId,
                                 Name = u.Name,
                                 Gender = u.Gender,
                                 PictureURL = u.PictureURL,
                                 RegistrationStatus = u.RegistrationStatus,
                                 Role = u.Role,
                                 VoteBalance = u.VoteBalance
                             };
                return result;
            }
            else
            {
                var result = from u in context.Users
                             where u.RegistrationStatus == RegistrationStatus
                             select new AllUsersStatus()
                             {
                                 Id = u.Id,
                                 IDPId = u.IDPId,
                                 Name = u.Name,
                                 Gender = u.Gender,
                                 PictureURL = u.PictureURL,
                                 RegistrationStatus = u.RegistrationStatus,
                                 Role = u.Role,
                                 VoteBalance = u.VoteBalance
                             };

                return result;
            }
        }


        /// <summary>
        /// 4.f. User Registration
        /// </summary>
        /// <param name="newrecord"></param>
        /// <returns></returns>
        public NewRegistration PostNew(DataObjects.User newrecord)
        {
            string status = "";
            try
            {
                var numrec = (from u in context.Users
                              where u.IDPId == newrecord.IDPId
                              select u).SingleOrDefault();
                status = numrec.RegistrationStatus;
            }
            catch
            {
                // If there is an error.... it means not already submitted.
                // Populate USERS table with the new info
                var NR = new DataObjects.User()
                {
                    Id = newrecord.Id,
                    IDPId = newrecord.IDPId,
                    Name = newrecord.Name,
                    Gender = newrecord.Gender,
                    PictureURL = newrecord.PictureURL,
                    Role = "User",
                    RegistrationStatus = "Pending",
                    VoteBalance = 0
                };
                context.Users.Add(NR);
                context.SaveChanges();

                var insert_new = new NewRegistration()
                {
                    ErrorCode = "2",
                    Message = "User with ID=" + newrecord.IDPId + " has just been submitted.",
                    RegistrationStatus = "Pending"
                };
                return insert_new;
            }

            // No error.... it means a record exists ... HTTP Status = CONFLICT
            var old = new NewRegistration
            {
                ErrorCode = "2",
                Message = "User with ID=" + newrecord.IDPId + " has already submitted.",
                RegistrationStatus = status,
            };
            return old;

        }


    }
}
