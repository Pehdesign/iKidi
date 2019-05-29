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
    public class SectionApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public async Task<string> GetAll()
        {
            List<Section> list = await db.Sections.ToListAsync();
            string response = "";
            foreach (Section element in list)
            {
                response = new JavaScriptSerializer().Serialize(element);
            }
            return response;
        }

        [HttpGet]
        public async Task<string> Get(int id)
        {
            Section section = await db.Sections.FindAsync(id);
            if (section == null)
            {
                return WebApiResponse.NOTFOUND;
            }
            string json = new JavaScriptSerializer().Serialize(section);
            return json;
        }

        [HttpPost]
        public HttpResponseMessage Post([FromBody] Section section)
        {
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
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