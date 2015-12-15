using MvcCustomer2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcCustomer2.Controllers
{
    //TODO:2.共用的BaseController
    public class BaseController : Controller
    {
        protected 客戶資料Entities db = new 客戶資料Entities();

        // GET: Base
        public BaseController()
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}