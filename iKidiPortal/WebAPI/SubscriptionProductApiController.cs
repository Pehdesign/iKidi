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
    public class SubscriptionProductApiController: ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> GetAll()
        {
            List<SubscriptionProduct> list = await db.SubscriptionProducts.ToListAsync();
            string response = "";
            foreach (SubscriptionProduct element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            SubscriptionProduct subscriptionProduct = await db.SubscriptionProducts.FindAsync(id);
            if (subscriptionProduct == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(subscriptionProduct);
            return json;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] SubscriptionProduct subscription)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                db.SubscriptionProducts.Add(subscription);
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