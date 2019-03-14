using iKidi.App_GlobalResources;
using iKidi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iKidi.Models
{
    public class UserSubscriptionViewModel
    {
        [Display(Name = "Subscriptions", ResourceType = typeof(Resource))]
        public ICollection<Subscription> Subscriptions { get; set; }
        public ICollection<UserSubscriptionModel> UserSubscriptions { get; set; }
        public bool DisableDropDown { get; set; }
        public string UserId { get; set; }
        public int SubscriptionId { get; set; }
    }
}