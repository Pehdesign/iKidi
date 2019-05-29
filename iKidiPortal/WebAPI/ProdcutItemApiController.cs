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
    public class ProductItemApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public async Task<string> GetAll()
        {
            List<ProductItem> list = await db.ProductItems.ToListAsync();
            string response = "";
            foreach (ProductItem element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            ProductItem productItem = await db.ProductItems.FindAsync(id);
            if (productItem == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(productItem);
            return json;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] ProductItem productItem)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                db.ProductItems.Add(productItem);
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