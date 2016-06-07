using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomAuthorization.CustomHelper
{
    public class CustomUserPrivilage
    {


        public string Controller;
        public string Action;
        public List<string> Parameters;
        public string ReturnType;
        public bool disabledStatus = false;
        public bool checkedStatus = false;

    }


    /*
public string CtrlName { get; set; }
public string ActName { get; set; }
public bool UserAccess { get; set; }
*/

}