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

    public class ItemApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        public async Task<string> GetAll()
        {
            List<Item> list = await db.Items.ToListAsync();
            string response = "";
            foreach (Item element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            Item item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(item);
            return json;
        }

        [HttpPost]
        public async Task<string> Create([FromBody] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                await db.SaveChangesAsync();
                return WebApiResponse.OK;
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(item);
                return json;
            }
        }
    }
}