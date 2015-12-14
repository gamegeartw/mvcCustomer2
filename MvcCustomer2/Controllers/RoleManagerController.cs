using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace MvcCustomer2.Controllers
{
    [Authorize(Roles ="admin",Users ="gamegear.tw@gmail.com")]
    public class RoleManagerController : Controller
    {
        private ApplicationRoleManager _roleManager;

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>(); }
            private set { _roleManager = value; }
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().Get<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public RoleManagerController() { }

        public RoleManagerController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        // GET: RoleManager
        public ActionResult Index()
        {
            List<MvcCustomer2.Models.AccountModel> users = new List<Models.AccountModel>();
            foreach(var user in UserManager.Users.ToList())
            {
                users.Add(new Models.AccountModel() { Id = user.Id, UserName = user.UserName, email = user.Email });
            }
            return View(users);
            //return View();
        }

        // GET: RoleManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleManager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleManager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleManager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
