using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class UsersView
    {
        public int ID { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int? UserType { get; set; }
        public DateTime? CreateDate { get; set; }


    }
}