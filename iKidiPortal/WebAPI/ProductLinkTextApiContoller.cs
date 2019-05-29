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
    public class ProductLinkTextApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> GetAll()
        {
            List<ProductLinkText> list = await db.ProductLinkTexts.ToListAsync();
            string response = "";
            foreach (ProductLinkText element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            ProductLinkText productLinkText = await db.ProductLinkTexts.FindAsync(id);
            if (productLinkText == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(productLinkText);
            return json;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] ProductLinkText productLinkText)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                db.ProductLinkTexts.Add(productLinkText);
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