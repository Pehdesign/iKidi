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
    public class UserSubscriptionApiController: ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> GetAll()
        {
            List<UserSubscription> list = await db.UserSubscriptions.ToListAsync();
            string response = "";
            foreach (UserSubscription element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            UserSubscription userSubscription = await db.UserSubscriptions.FindAsync(id);
            if (userSubscription == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(userSubscription);
            return json;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] UserSubscription userSubscription)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                db.UserSubscriptions.Add(userSubscription);
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