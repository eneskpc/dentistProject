using DentistProject.Helpers;
using DentistProject.Models;
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
            return (HttpContext.Current.Session["UserEmail"] != null);
        }

        public static bool Login(string userName, string password)
        {
            using (DBEntities db = new DBEntities())
            {
                string encyptedPassword = CryptoHelper.EncrytCustomMD5(password);
                Users user = (from u in db.Users
                              where u.IsDeleted == false
                              where u.Password == encyptedPassword
                              where u.UserEmail == userName
                              select u).FirstOrDefault();
                if (user != null)
                {
                    HttpContext.Current.Session["UserEmail"] = user.UserEmail;
                    return true;
                }
                return false;
            }
        }
    }
}