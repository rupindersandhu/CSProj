using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthorization.Controllers
{
    public class ErrHandlerController : Controller
    {
        // GET: ErrHandler
        public ActionResult Index(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }
    }
}