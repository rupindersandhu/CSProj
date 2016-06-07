using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Xml;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Web.Mvc;
using Newtonsoft.Json;
using CustomAuthorization.CustomHelper;

namespace CustomAuthorization.Models
{
    public static class CustomAuth
    {

        public static List<string> attributeExtractor(ParameterInfo[] parameterList)
        {
            List<string> paramList = new List<string>();

            if(parameterList.Length == 0)
            {
                paramList.Add("No Parameters");
                return paramList;
            }

            foreach(ParameterInfo parameter in parameterList)
            {
                paramList.Add(parameter.ParameterType.ToString()); 
            }

            return paramList;
        }

        

        public static bool isAllowed(Controller thisObject, string accessPrivileges)
        {

            // get code to find out which controller and action is being called
            string actionName = thisObject.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = thisObject.ControllerContext.RouteData.Values["controller"].ToString();

            controllerName += "Controller";

            AssemblyCompositionVM customVM = getAssemblyCompositionVM(accessPrivileges);

            foreach(CustomUserPrivilage privilage in customVM.PrivilageStructs)
            {
                if(privilage.Controller == controllerName && privilage.Action == actionName)
                {
                    if(privilage.checkedStatus == true)
                    {
                        return true;
                    }
                }
            }

            // parse accessPrivileges json string containing controller and 
            // action names that the user can currently access
            // if the current action and controller exists in the above string return true else false
            return false;
        }
        
        

       
        public static string getAssemblyComposition()
        {
    
             Assembly asm = Assembly.GetAssembly(typeof(CustomAuthorization.MvcApplication));

            // var is List<CustomHelper.CustomUserPrivilage>
            var controlleractionlist = asm.GetTypes()
        
            .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
            .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))

            .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
            //.Where(m => typeof(System.Web.Mvc.ActionResult).IsAssignableFrom(m.ReturnType) && m.GetParameters().Any(p => typeof(Type).IsAssignableFrom(p.ParameterType)))

            //.Select(x => new CustomHelper.CustomUserPrivilage(){ Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })

            .Select(x => new CustomHelper.CustomUserPrivilage()
                            {
                                Controller = x.DeclaringType.Name,
                                Action = x.Name,
                                ReturnType = x.ReturnType.Name,
                                Parameters = Models.CustomAuth.attributeExtractor(x.GetParameters()) })

            .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();

            /*
                    List<string> controllerNames = new List<string>();
                    Assembly.GetCallingAssembly().GetTypes().
                        Where(type => type.IsSubclassOf(typeof(Controller))).ToList().
                        ForEach(type => controllerNames.Add(type.Name));

                    return controllerNames;

            */

            // returns a json containing all the controllers and 
            // their action methods in the assembly named in the web.config file.

            var json = JsonConvert.SerializeObject(controlleractionlist);

            return json;
        }

        
        public static AssemblyCompositionVM getAssemblyCompositionVM()
        {

            // returns a view model containing all the controllers and 
            // their action methods in the assembly named in the web.config file.

            string json = getAssemblyComposition();

            List <CustomUserPrivilage> privilageList = JsonConvert.DeserializeObject<List<CustomHelper.CustomUserPrivilage>>(json);

            CustomUserPrivilage[] privilageArray = (from item in privilageList select item as CustomHelper.CustomUserPrivilage).ToArray();

            AssemblyCompositionVM customVM = new AssemblyCompositionVM(privilageArray);

            return customVM;

        }


        public static AssemblyCompositionVM getAssemblyCompositionVM(string json)
        {

            // returns a view model containing all the controllers and 
            // their action methods in the assembly based on the access Privilage of the user.

            List<CustomUserPrivilage> privilageList = JsonConvert.DeserializeObject<List<CustomHelper.CustomUserPrivilage>>(json);

            CustomUserPrivilage[] privilageArray = (from item in privilageList select item as CustomHelper.CustomUserPrivilage).ToArray();

            AssemblyCompositionVM customVM = new AssemblyCompositionVM(privilageArray);

            return customVM;

        }

    }
}