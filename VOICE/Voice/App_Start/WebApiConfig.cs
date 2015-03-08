using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using Voice.DataObjects;
using Voice.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Voice
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            /// See at least 12 Features
        List<Feature> featureItems = new List<Feature>
            {
        new Feature { Id = Guid.NewGuid().ToString(), Title = "1", Description = "User Interface" },
        new Feature { Id = Guid.NewGuid().ToString(), Title = "2", Description = "Color Combination" },
        new Feature { Id = Guid.NewGuid().ToString(), Title = "3", Description = "Performance" },
        new Feature { Id = Guid.NewGuid().ToString(), Title = "4", Description = "Navigation" },
        new Feature { Id = Guid.NewGuid().ToString(), Title = "5", Description = "Relevance" },
        new Feature { Id = Guid.NewGuid().ToString(), Title = "6", Description = "Predictability" },
        new Feature { Id = Guid.NewGuid().ToString(), Title = "7", Description = "Documentation" },

        };

            foreach (Feature Item in featureItems)
            {
                context.Set<Feature>().Add(Item);
            }
            base.Seed(context);


            /// Seed the Initial User for TA testing
        List<User> userItems = new List<User>
            {
        new User { Id = Guid.NewGuid().ToString(), IDPId= "Twitter:3002607419",
                        Name="CSCI64-01", Gender=null,
                        PictureURL= "https://pbs.twimg.com/profile_images/56q4525235/_bn5SQ65X_normal.jpg",
                        VoteBalance = 100,  RegistrationStatus = "Pending", Role="Administrator",
                  }
            };

            foreach (User Item in userItems)
            {
                context.Set<User>().Add(Item);
            }
            base.Seed(context);
        
        
        
        
        
        
        }




    }
}

