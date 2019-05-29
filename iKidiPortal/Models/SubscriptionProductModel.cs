using iKidi.App_GlobalResources;
using iKidi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iKidi.Models
{
    public class SubscriptionProductModel
    {
        [Display(Name = "ProductId", ResourceType = typeof(Resource))]
        public int ProductId { get; set; }

        [Display(Name = "SubscriptionId", ResourceType = typeof(Resource))]
        public int SubscriptionId { get; set; }

        [Display(Name = "ProductTitle", ResourceType = typeof(Resource))]
        public string ProductTitle { get; set; }

        [Display(Name = "SubscriptionTitle", ResourceType = typeof(Resource))]
        public string SubscriptionTitle { get; set; }

        [Display(Name = "Product", ResourceType = typeof(Resource))]
        public ICollection<Product> Products { get; set; }

        [Display(Name = "Subscriptions", ResourceType = typeof(Resource))]
        public ICollection<SubscriptionProduct> Subscriptions { get; set; }
    }
}