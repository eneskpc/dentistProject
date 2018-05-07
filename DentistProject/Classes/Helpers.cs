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
            return (HttpContext.Current.Session["DoctorInfo"] != null || HttpContext.Current.Session["AsistantInfo"] != null || HttpContext.Current.Session["PatientInfo"] != null);
        }
    }
}