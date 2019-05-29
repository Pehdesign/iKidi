using iKidi.Entities;
using iKidi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace iKidi.WebAPI
{
    public class SubscriptionApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> GetAll()
        {
            List<Subscription> list = await db.Subscriptions.ToListAsync();
            string response = "";
            foreach (Subscription element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            Subscription subscription = await db.Subscriptions.FindAsync(id);
            if (subscription == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(subscription);
            return json;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Subscription subscription)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                db.Subscriptions.Add(subscription);
                db.SaveChangesAsync();
                response = Request.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotAcceptable);
                return response;
            }
        }
    }
}