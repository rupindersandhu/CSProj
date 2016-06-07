using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomAuthorization.Models
{
    public class CSEAgent
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string AccessPrivilages { get; set; }
        public string Name { get; set; }
        public string ClearanceLevel { get; set; }
        public string Branch { get; set; }

        //public IEnumerable<SelectListItem> ClearanceList
        //{
        //    get
        //    {
        //        return new List<SelectListItem>
        //        {
        //            new SelectListItem { Text = "Employee", Value = "1"},
        //            new SelectListItem { Text = "Rugby", Value = "2"},
        //            new SelectListItem { Text = "Manager", Value = "3"},
        //            new SelectListItem { Text = "Administrator", Value = "4"}
        //        };
        //    }
        //}

        //public IEnumerable<SelectListItem> BranchList
        //{
        //    get
        //    {
        //        return new List<SelectListItem>
        //        {
        //            new SelectListItem { Text = "Surrey", Value = "Football"},
        //            new SelectListItem { Text = "Burnaby", Value = "Rugby"},
        //            new SelectListItem { Text = "Vancouver", Value = "Horse Racing"}
        //        };
        //    }
        //}
    }
}