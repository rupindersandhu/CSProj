using CustomAuthorization.CustomHelper;
using CustomAuthorization.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CustomAuthorization.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            string json = Models.CustomAuth.getAssemblyComposition();

            ViewBag.Aptt2 = json;

            //privilages.PrivilageList = JsonConvert.DeserializeObject<List<CustomHelper.CustomUserPrivilage>>(json);

            //CustomHelper.CustomUserPrivilage[] arr = (CustomHelper.CustomUserPrivilage[])JsonConvert.DeserializeObject(json);
            //return View(privilages.PrivilageList);

            //var myObjectArray = (from item in privilages.PrivilageList select item as CustomHelper.CustomUserPrivilage).ToArray();

            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
    }
}