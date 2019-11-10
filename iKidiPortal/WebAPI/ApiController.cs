using System;
using iKidi.Entities;
using iKidi.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using iKidi.Extensions;
using System.Data.Entity;
using System.Web.Script.Serialization;
using iKidi.Controllers;
using iKidi.App_GlobalResources;

namespace iKidi.WebAPI
{
    [System.Web.Http.Authorize]
    public class ApiController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApiController()
        {
        }
        public ApiController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


         // POST: /Api/Login
            [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        public async Task<string> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return "Invalid login attempt";
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return "Successful";                    
                case SignInStatus.LockedOut:
                    return "User is locked out";
                case SignInStatus.RequiresVerification:
                    return "User required verification";
                case SignInStatus.Failure:
                    return "Invalid login attempt";
                default:
                    return "Invalid login attempt";
            }
        }

         // POST: /Api/Logout
        [System.Web.Mvc.HttpPost]
        public async Task<string> Logout()
        {
            try
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return "Successful logout";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }


        // POST Api/Home
        [System.Web.Mvc.HttpPost]
        public async Task<string> Home()
        {
            var userId = Request.IsAuthenticated ? HttpContext.User.Identity.GetUserId() : null;
            var thumbnails = await new List<ThumbnailModel>().GetProductThumbnailsAsync(userId);
            var count = thumbnails.Count() / 4;
            var thumbnailsWithTitle
                = new List<ThumbnailAreaModel>();
            for (int i = 0; i <= count; i++)
            {
                thumbnailsWithTitle.Add(new ThumbnailAreaModel
                {
                    Title = i.Equals(0) ? @Resource.MyContent : string.Empty,
                    Thumbnails = thumbnails.Skip(i * 4).Take(4)
                });
            }
            var username = Request.IsAuthenticated ? HttpContext.User.Identity.GetUserName() : null;
            var json = new JavaScriptSerializer().Serialize(thumbnailsWithTitle[0].Thumbnails);
            var data = "[{\"Username\": \"" + username + "\"}," + json.Substring(1);
            return data;
        }

        // POST Api/Club
        [System.Web.Mvc.HttpPost]
        public async Task<string> Club(int id)
        {
            var userId = Request.IsAuthenticated ? HttpContext.GetUserId() : null;
            var sections = await SectionExtensions.GetProductSectionsAsync(id, userId);
            var username = Request.IsAuthenticated ? HttpContext.User.Identity.GetUserName() : null;
            var json = new JavaScriptSerializer().Serialize(sections);
            return json;
        }

    }




   

}