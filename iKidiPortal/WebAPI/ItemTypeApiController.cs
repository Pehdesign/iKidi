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
    public class ItemTypeApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> GetAll()
        {
            List<ItemType> list = await db.ItemTypes.ToListAsync();
            string response = "";
            foreach (ItemType element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            ItemType itemType = await db.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(itemType);
            return json;
        }

        [HttpPost]
        public async Task<string> Create([FromBody] ItemType itemType)
        {
            if (ModelState.IsValid)
            {
                db.ItemTypes.Add(itemType);
                await db.SaveChangesAsync();
                return WebApiResponse.OK;
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(itemType);
                return json;
            }
        }
    }
}