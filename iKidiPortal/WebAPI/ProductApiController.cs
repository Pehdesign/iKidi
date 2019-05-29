using System;
using iKidi.Entities;
using iKidi.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;

namespace iKidi.WebAPI
{
    [Authorize(Roles = "Admin")]
    public class ProductApiController : ApiController
   {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> GetAll()
        {
            List<Product> list = await db.Products.ToListAsync();
            string response = "";
            foreach (Product element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(product);
            return json;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Product product)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
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