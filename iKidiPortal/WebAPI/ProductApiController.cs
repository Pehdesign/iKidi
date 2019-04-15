namespace iKidi.WebAPI
{
    using iKidi.Entities;
    using iKidi.Models;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Transactions;
    using System.Web.Http;
    using System.Web.Script.Serialization;

    [Authorize(Roles = "Admin")]
    public class ProductApiController : ApiController
   {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const string OK = "OK";
        private const string BADREQUEST = "BAD REQUEST";
        private const string NOTFOUND = "NOT FOUND";

        [HttpGet]
        public async Task<string> Get(string title)
        {
            var db = ApplicationDbContext.Create();
            Product product = await (Product)from p in db.Products
                                       where p.Title.CompareTo(title) == 0
                                       select p;
            if(product == null)
            {
                return NOTFOUND;
            }
            var json = new JavaScriptSerializer().Serialize(product);           
            return json;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            if(id == null)
            {
                return BADREQUEST;
            }
            Product product = await (Product)from p in db.Products
                                       where p.Id.CompareTo(id) == 0
                                       select p;
            if (product == null)
            {
                return NOTFOUND;
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
                return OK;
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
                    return OK;
                }
                catch
                {
                    transaction.Dispose();
                    return NOTFOUND;
                }
            }
        }
    }
}