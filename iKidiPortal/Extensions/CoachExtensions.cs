using iKidi.Entities;
using iKidi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace iKidi.Extensions
{
    public static class CoachExtensions
    {
        public static async Task<List<int>> GetSubscriptionIdsAsync(
            string userId = null, ApplicationDbContext db = null)
        {
            try
            {
                if (userId == null) return new List<int>();
                if (db == null) db = ApplicationDbContext.Create();

                return await (
                    from us in db.UserSubscriptions
                    where us.UserId.Equals(userId)
                    select us.SubscriptionId).ToListAsync();
            }
            catch { }

            return new List<int>();
        }
        public static async Task<List<int>> GetProductIdsAsync(
    string subscriptionId = null, ApplicationDbContext db = null)
        {
            try
            {
                if (subscriptionId == null) return new List<int>();
                if (db == null) db = ApplicationDbContext.Create();

                return await (
                    from sp in db.SubscriptionProducts
                    where sp.SubscriptionId.Equals(subscriptionId)
                    select sp.ProductId).ToListAsync();
            }
            catch { }

            return new List<int>();
        }

        public static async Task<List<Product>> GetProductsAsync(string userId = null, ApplicationDbContext db = null)
        {
            var model = new Product();

            if (userId == null) return new List<Product>();
            if (db == null) db = ApplicationDbContext.Create();

            var subscriptionIds = await GetSubscriptionIdsAsync(userId, db);
            var productIds = await GetProductIdsAsync(userId, db);
            try
            {
                return await (from p in db.Products
                              where p.Id.Equals(productIds)
                              select new Product
                              {
                                  Id = p.Id,
                                  Title = p.Title,
                                  Description = p.Description,
                                  ImageUrl = p.ImageUrl,
                                  ProductLinkTextId = p.ProductLinkTextId,
                                  ProductTypeId = p.ProductTypeId
                              }).ToListAsync();
            }
            catch { }
            return new List<Product>();
        }

    }


}