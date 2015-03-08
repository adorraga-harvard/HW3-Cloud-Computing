using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service;

namespace Voice.Controllers
{
    public class featurevoteController : ApiController
    {
        public ApiServices Services { get; set; }
        Models.MobileServiceContext context = new Models.MobileServiceContext();
        
        // GET api/featurevote
        public IQueryable<Pattern.featureList> Get()
        {
            Services.Log.Info("Hello from featurevote controller!");
            var result = from f in context.Features
                         select new Pattern.featureList()
                         {
                             featureId = f.Id,
                             Title = f.Title,
                             Description = f.Description,
                             Votes = f.Votes,
                         };
            return result;
        }

    }
}
