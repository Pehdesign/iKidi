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

namespace iKidi.WebAPI
{
    [Authorize(Roles = "Admin")]
    public class ProductApiController : ApiController
   {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> Get(string title)
        {
            Product product = await (Product)from p in db.Products
                                       where p.Title.CompareTo(title) == 0
                                       select p;
            if(product == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            var json = new JavaScriptSerializer().Serialize(product);           
            return json;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            if(id == null)
            {
                return WebApiResponse.BADREQUEST;
            }
            Product product = await (Product)from p in db.Products
                                       where p.Id.CompareTo(id) == 0
                                       select p;
            if (product == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            var json = new JavaScriptSerializer().Serialize(product);
            return json;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return WebApiResponse.OK;
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(product);
                return json;
            }
        }

        [HttpDelete]
        public async Task<string> Delete(int id)
        {
            Product product = await (Product)from p in db.Products
                                       where p.Id.CompareTo(id) == 0
                                       select p;
            using (var transaction = new TransactionScope(
                    TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var prodItems = db.ProductItems.Where(
                        pi => pi.ProductId.Equals(id));
                    var prodSubscr = db.SubscriptionProducts.Where(
                        sp => sp.ProductId.Equals(id));
                    db.ProductItems.RemoveRange(prodItems);
                    db.SubscriptionProducts.RemoveRange(prodSubscr);
                    db.Products.Remove(product);
                    await db.SaveChangesAsync();
                    transaction.Complete();
                    return WebApiResponse.OK;
                }
                catch
                {
                    transaction.Dispose();
                    return WebApiResponse.NOTFOUND;
                }
            }
        }
    }
}