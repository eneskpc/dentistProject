using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class Helpers
    {
        public static bool CheckAuthentication()
        {
            return (HttpContext.Current.Session["UserName"] != null && HttpContext.Current.Session["Password"] != null);
        }

        public static bool Login(string userName, string password)
        {
            if (userName == "Admin" && password == "123")
            {
                HttpContext.Current.Session["UserName"] = "Admin";
                HttpContext.Current.Session["Password"] = "123";
                return true;
            }
            return false;
        }
    }
}