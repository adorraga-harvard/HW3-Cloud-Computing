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
    public class featureController : TableController<Feature>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Feature>(context, Request, Services);
        }

        // GET tables/Feature
        public IQueryable<Feature> GetAllFeature()
        {
            return Query();
        }

        // GET tables/Feature/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Feature> GetFeature(string id)
        {
            return Lookup(id);
        }

       
    }
}