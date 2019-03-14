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
    public class ProductItemModel
    {
        [Display(Name = "ProductId", ResourceType = typeof(Resource))]
        public int ProductId { get; set; }

        [Display(Name = "ItemId", ResourceType = typeof(Resource))]
        public int ItemId { get; set; }

        [Display(Name = "ProductTitle", ResourceType = typeof(Resource))]
        public string ProductTitle { get; set; }

        [Display(Name = "ItemTitle", ResourceType = typeof(Resource))]
        public string ItemTitle { get; set; }

        [Display(Name = "Products", ResourceType = typeof(Resource))]
        public ICollection<Product> Products { get; set; }

        [Display(Name = "Items", ResourceType = typeof(Resource))]
        public ICollection<Item> Items { get; set; }
    }
}