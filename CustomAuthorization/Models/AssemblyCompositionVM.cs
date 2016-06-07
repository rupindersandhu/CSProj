using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace CustomAuthorization.Models
{
    public class AssemblyCompositionVM
    {
        public Guid UserId { get; set; }

        public int Count { get; set; }

        public string ReturnPriv { get; set; }

        public CustomHelper.CustomUserPrivilage[] PrivilageStructs { get; set; }


        public AssemblyCompositionVM(CustomHelper.CustomUserPrivilage[] arr)
        {
            PrivilageStructs = arr;
            Count = arr.Count();
        }

        public AssemblyCompositionVM()
        {
         
        }

    }
}