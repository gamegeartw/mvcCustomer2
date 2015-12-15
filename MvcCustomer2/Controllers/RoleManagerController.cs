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
    [Authorize(Roles = "admin")]
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

        public RoleManagerController()
        {
        }

        //TODO:注入UserManager跟RoleManager
        public RoleManagerController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: RoleManager
        public ActionResult Index()
        {
            List<MvcCustomer2.Models.AccountModel> users = new List<Models.AccountModel>();
            foreach (var user in UserManager.Users.ToList())
            {
                users.Add(GetUser(user.Id));
            }
            return View(users);
            //return View();
        }

        // GET: RoleManager/Details/5
        public ActionResult Details(string id)
        {

            return View(GetUser(id));
        }

        // GET: RoleManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleManager/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "Id,UserName,Email,Password,UserRoles")]Models.AccountModel account)
        {
            try
            {
                if (true)
                {

                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RoleManager/Edit/5
        public ActionResult Edit(string id)
        {
            var _user = GetUser(id);
            return View(_user);
        }

        //TODO:自訂取得使用者方法
        private Models.AccountModel GetUser(string id)
        {
            var user = UserManager.FindById(id);
            var _user = new Models.AccountModel() { Id = user.Id, UserName = user.UserName, Email = user.Email };
            _user.UserRoles = UserManager.GetRoles(user.Id);
            return _user;
        }

        // POST: RoleManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Email,UserRoles")]Models.AccountModel account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add update logic here
                    var user = UserManager.FindById(account.Id);
                    user.UserName = account.UserName;
                    user.Email = account.Email;
                    //TODO:先將帳號自角色群組中全數移除，再加入指定的角色內
                    UserManager.RemoveFromRoles(account.Id, UserManager.GetRoles(account.Id).ToArray());
                    UserManager.AddToRoles(account.Id, account.UserRoles.ToArray());
                    UserManager.Update(user);//一定要加，常常忘記 XDDD
                }
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: RoleManager/Delete/5
        public ActionResult Delete(string id)
        {
            return View(GetUser(id));
        }

        // POST: RoleManager/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    UserManager.Delete(UserManager.FindById(id));
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
