using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iKidi.Models;
using iKidi.Extensions;
using Microsoft.AspNet.Identity;
using iKidi.Entities;

namespace iKidi.Controllers
{
    public class CoachController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Coach
        public async Task<ActionResult> Index()
        {
            var userId = Request.IsAuthenticated ? HttpContext.User.Identity.GetUserId() : null;

            var subscriptions = await (from us in db.UserSubscriptions
                                where us.UserId.Equals(userId)
                                select us.SubscriptionId).ToListAsync();

            string desc = ""; 
            for (int i = 0; i < subscriptions.Count; i++) {
                desc = desc + subscriptions[i];
            }
            string prod = "";
            var productsList = new List<List<int>>();
            var products = new List<int>();

            for (int i = 0; i < subscriptions.Count; i++)
            {
                var myInt = subscriptions[i];
                products = await (
                        from sp in db.SubscriptionProducts
                        where sp.SubscriptionId.Equals(myInt)
                        select sp.ProductId).ToListAsync();
                productsList.Add(products);
            }
            for (int i = 0; i < productsList.Count; i++)
            {
                prod = prod + subscriptions[i] + ",";
            }
            //var product = CoachExtensions.GetProductsAsync();

            var model = new CoachModel
            {
                Description=desc,
                HTML=prod,
                ItemTypes = db.ItemTypes.ToList(),
                Parts = db.Parts.ToList(),
                Sections = db.Sections.ToList()
            };


          



            return View(model);
        }
    }
}
