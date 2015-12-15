using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Net;

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

        //TODO:注入UserManager跟RoleManager(finish)
        public RoleManagerController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: RoleManager
        public ActionResult Index(string message)
        {
            List<MvcCustomer2.Models.AccountModel> users = new List<Models.AccountModel>();
            foreach (var user in UserManager.Users.ToList())
            {
                users.Add(GetUser(user.Id));
            }
            ViewBag.StatusMessage = message;
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
        //TODO:使用Bind時要特別注意，當Model被設定為Required時，欄位一定要有值，否則ModelState.IsValid會是False
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,UserName,Password,NickName,UserRoles")]Models.AccountModel account)
        {
            String message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: 由管理員建立帳號
                    var user = new Models.ApplicationUser() { UserName = account.Email, Email = account.Email, NickName = account.NickName };
                    UserManager.Create(user,"P@ssw0rd");
                    UserManager.AddToRole(UserManager.FindByEmail(account.Email).Id, "user");
                    message=@"已建立帳號，密碼為P@ssw0rd";
                }
                
            }
            catch(Exception ex)
            {
                message=ex.Message;
            }
            return RedirectToAction("Index", new { message = message });
        }

        // GET: RoleManager/Edit/5
        public ActionResult Edit(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var _user = GetUser(id);
                ViewBag.Roles = RoleManager.Roles.Select(r => r.Name).ToList();
                return View(_user);
            }

        }

        //TODO:自訂取得使用者方法(finish)
        private Models.AccountModel GetUser(string id)
        {
            Models.AccountModel _user = new Models.AccountModel(); ;
            try
            {
                var user = UserManager.FindById(id);
                 _user = new Models.AccountModel() { Id = user.Id, UserName = user.UserName, Email = user.Email,NickName=user.NickName };
                
                _user.UserRoles = UserManager.GetRoles(user.Id);
                
            }
            catch (Exception)
            {
                
               // throw;
                RedirectToAction("Index");
            }
            return _user;
        }

        // POST: RoleManager/Edit/5
        //TODO:對帳號角色來做編輯(finish)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Email,NickName,UserRoles")]Models.AccountModel account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: 更新帳號資料(管理員專用)
                    var user = UserManager.FindById(account.Id);
                    user.UserName = account.UserName;
                    user.Email = account.Email;
                    //因為取得的UserRoles是一個從CheckBox取得的陣列,有選取到才會有值,不然都是false,
                    //所以用linq或是Lambda取得非false的字串
                    user.NickName = account.NickName;
                    var result = from r in account.UserRoles
                                 where r != "false"
                                 select r;
                    var result2 = account.UserRoles.Where((string x) => x != "false");//這裡要明確指定變數型別(因為自動推斷會推斷成boolean)


                    //TODO:先將帳號自角色群組中全數移除，再加入指定的角色內
                    UserManager.RemoveFromRoles(account.Id, UserManager.GetRoles(account.Id).ToArray());
                    if (result != null)
                    {
                        UserManager.AddToRoles(account.Id, result2.ToArray());
                    }

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                // TODO: 刪除帳號(管理員專用)
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
        [HttpPost, ActionName("ResetPassword")]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPasswordConfirmed(string Id)
        {
            //TODO:重置密碼，預設密碼使用appSetting來指定(finish)
            UserManager.RemovePassword(Id);
            var password = System.Web.Configuration.WebConfigurationManager.AppSettings.GetValues("ResetPassword")[0];
            UserManager.AddPassword(Id, password);
            return RedirectToAction("Index");
        }
        public ActionResult ResetPassword(string id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
