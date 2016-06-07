using CustomAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthorization.Controllers
{
    public class AdminForumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AdminForum
        public ActionResult Index()
        {
            if (Session["agentId"] == null)
            {
                return RedirectToAction("Index", "ErrHandler", new { msg = "Authentication Error, Please Login." });
            }
            else
            {
                CSEAgent cSEAgent = db.CSEAgents.Find(Session["agentId"]);

                if (CustomAuth.isAllowed(this, cSEAgent.AccessPrivilages))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "ErrHandler", new { msg = "Authorization Error, Please Contact the Aministrator." });
                }
            }
        }


        public ActionResult ARoom1()
        {
            if (Session["agentId"] == null)
            {
                return RedirectToAction("Index", "ErrHandler", new { msg = "Authentication Error, Please Login." });
            }
            else
            {
                CSEAgent cSEAgent = db.CSEAgents.Find(Session["agentId"]);

                if (CustomAuth.isAllowed(this, cSEAgent.AccessPrivilages))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "ErrHandler", new { msg = "Authorization Error, Please Contact the Aministrator." });
                }
            }
        }

        public ActionResult ARoom2()
        {
            if (Session["agentId"] == null)
            {
                return RedirectToAction("Index", "ErrHandler", new { msg = "Authentication Error, Please Login." });
            }
            else
            {
                CSEAgent cSEAgent = db.CSEAgents.Find(Session["agentId"]);

                if (CustomAuth.isAllowed(this, cSEAgent.AccessPrivilages))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "ErrHandler", new { msg = "Authorization Error, Please Contact the Aministrator." });
                }
            }
        }

        public ActionResult ARoom3()
        {
            if (Session["agentId"] == null)
            {
                return RedirectToAction("Index", "ErrHandler", new { msg = "Authentication Error, Please Login." });
            }
            else
            {
                CSEAgent cSEAgent = db.CSEAgents.Find(Session["agentId"]);

                if (CustomAuth.isAllowed(this, cSEAgent.AccessPrivilages))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "ErrHandler", new { msg = "Authorization Error, Please Contact the Aministrator." });
                }
            }
        }

        public ActionResult ARoom4()
        {
            if (Session["agentId"] == null)
            {
                return RedirectToAction("Index", "ErrHandler", new { msg = "Authentication Error, Please Login." });
            }
            else
            {
                CSEAgent cSEAgent = db.CSEAgents.Find(Session["agentId"]);

                if (CustomAuth.isAllowed(this, cSEAgent.AccessPrivilages))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "ErrHandler", new { msg = "Authorization Error, Please Contact the Aministrator." });
                }
            }
        }
    }
}