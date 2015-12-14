using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcCustomer2.Models;

namespace MvcCustomer2.Controllers
{
    //TODO:建立帳號管理共用Controller
    public class AccountBaseController : Controller
    {
        protected ApplicationSignInManager _signInManager;
        protected ApplicationUserManager _userManager;
        //TODO:建立Identity EntityFramework實體
        protected ApplicationDbContext context = new ApplicationDbContext();

        public AccountBaseController()
        {
            //TODO:新增預設角色
            if (context.Roles.Count()==0)
            {
                context.Roles.Add(new IdentityRole("admin"));
                context.Roles.Add(new IdentityRole("operator"));
                context.Roles.Add(new IdentityRole("user"));
            }
            context.SaveChanges();

        }
        public AccountBaseController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager  != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }
            if (disposing && _signInManager !=null)
            {
                _signInManager.Dispose();
                _signInManager = null;
            }
            if (disposing && context != null)
            {
                context.Dispose();
                context = null;

            }
            base.Dispose(disposing);
        }
    }
}