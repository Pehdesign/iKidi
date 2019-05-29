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
    public class PartApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> GetAll()
        {
            List<Part> list = await db.Parts.ToListAsync();
            string response = "";
            foreach (Part element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            Part part = await db.Parts.FindAsync(id);
            if (part == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(part);
            return json;
        }

        [HttpPost]
        public async Task<string> Create([FromBody] Part part)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(part);
                await db.SaveChangesAsync();
                return WebApiResponse.OK;
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(part);
                return json;
            }
        }
    }
}