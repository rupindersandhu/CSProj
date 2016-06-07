using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomAuthorization.Models;
using Newtonsoft.Json;
using CustomAuthorization.CustomHelper;

namespace CustomAuthorization.Controllers
{
    public class CSEAgentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CSEAgents
        public ActionResult Index()
        {

            if (Session["agentId"] == null)
            {
                return RedirectToAction("Index", "ErrHandler", new{ msg = "Authentication Error," });
            }
            else
            {
                CSEAgent cSEAgent = db.CSEAgents.Find(Session["agentId"]);

                if(Int32.Parse(cSEAgent.ClearanceLevel) < 2)
                {
                    return RedirectToAction("Index", "ErrHandler", new { msg = "Authorization Error, Please Contact the Aministrator." });
                }

                List<CustomAuthorization.Models.CSEAgent> nonModList = db.CSEAgents.ToList();

                List<CustomAuthorization.Models.CSEAgent> modList = new List<CSEAgent>();

                foreach (var agent in nonModList)
                {
                    if(Int32.Parse(agent.ClearanceLevel) < Int32.Parse(cSEAgent.ClearanceLevel))
                    {
                        modList.Add(agent);
                    }
                }

                return View(modList);
            }

            
            
        }

        // GET: CSEAgents/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSEAgent cSEAgent = db.CSEAgents.Find(id);
            if (cSEAgent == null)
            {
                return HttpNotFound();
            }
            return View(cSEAgent);
        }

        // GET: CSEAgents/Create
        public ActionResult Create()
        {
            CSEAgent cSEAgent = db.CSEAgents.Find(Session["agentId"]);

            ViewBag.cLevel = Int32.Parse(cSEAgent.ClearanceLevel);
            return View();
        }

        // POST: CSEAgents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,AccessPrivilages,Name,ClearanceLevel,Branch")] CSEAgent cSEAgent)
        {
            if (ModelState.IsValid)
            {
                cSEAgent.Id = Guid.NewGuid();


                //cSEAgent.AccessPrivilages = CustomAuth.getAssemblyComposition();

                cSEAgent.AccessPrivilages = getUserPrivilages();

                db.CSEAgents.Add(cSEAgent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cSEAgent);
        }

        public string getUserPrivilages()
        {
            CSEAgent cSEAgent = db.CSEAgents.Find(Session["agentId"]);

            AssemblyCompositionVM customVM = CustomAuth.getAssemblyCompositionVM(cSEAgent.AccessPrivilages);

            foreach(CustomUserPrivilage privilage in customVM.PrivilageStructs)
            {
                if(privilage.checkedStatus == false)
                {
                    privilage.disabledStatus = true;
                }
                else
                {
                    privilage.checkedStatus = false;
                }
            }

            return JsonConvert.SerializeObject(customVM.PrivilageStructs.ToList<CustomUserPrivilage>());

        }

        // GET: CSEAgents/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CSEAgent cSEAgent = db.CSEAgents.Find(id);
            if (cSEAgent == null)
            {
                return HttpNotFound();
            }

            CSEAgent loggedInAgent = db.CSEAgents.Find(Session["agentId"]);

            if (Int32.Parse(loggedInAgent.ClearanceLevel) < 3)
            {
                if(loggedInAgent.Branch != cSEAgent.Branch)
                {
                    return RedirectToAction("Index", "ErrHandler", new { msg = "Authorization Error, Cannot Edit Employee from other branch, Please Contact the Aministrator." });
                }
            }

            return View(cSEAgent);
        }

        // POST: CSEAgents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,AccessPrivilages,Name,ClearanceLevel,Branch")] CSEAgent cSEAgent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cSEAgent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cSEAgent);
        }

        // GET: CSEAgents/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSEAgent cSEAgent = db.CSEAgents.Find(id);
            if (cSEAgent == null)
            {
                return HttpNotFound();
            }


            CSEAgent loggedInAgent = db.CSEAgents.Find(Session["agentId"]);

            if (Int32.Parse(loggedInAgent.ClearanceLevel) < 3)
            {
                if (loggedInAgent.Branch != cSEAgent.Branch)
                {
                    return RedirectToAction("Index", "ErrHandler", new { msg = "Authorization Error, Cannot Delete Employee from other branch, Please Contact the Aministrator." });
                }
            }

            return View(cSEAgent);
        }

        // POST: CSEAgents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CSEAgent cSEAgent = db.CSEAgents.Find(id);
            db.CSEAgents.Remove(cSEAgent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


       public ActionResult EditPrivilages(Guid? id)
       {

            // retrieve object
            //          accessPrivilages

            // calls getAssemblyCompositionVM 
            //                               finds controllers
            //                                      actions
            //                                                  of assemblies

            // return a View
            //              lists acessable actions and controllers
            //              with checkboxes.


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CSEAgent cSEAgent = db.CSEAgents.Find(id);
            if (cSEAgent == null)
            {
                return HttpNotFound();
            }

            string accessPrivilage = cSEAgent.AccessPrivilages;

            AssemblyCompositionVM customVM = CustomAuth.getAssemblyCompositionVM(accessPrivilage);

            customVM.UserId = cSEAgent.Id;

            return View(customVM);

        }


        [HttpPost]
        public ActionResult SavePrivilages(AssemblyCompositionVM collection)
        {
    
            if (collection.UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CSEAgent cSEAgent = db.CSEAgents.Find(collection.UserId);

            if (cSEAgent == null)
            {
                return HttpNotFound();
            }

            cSEAgent.AccessPrivilages = collection.ReturnPriv; 

            db.Entry(cSEAgent).State = EntityState.Modified;
            db.SaveChanges();


            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}
