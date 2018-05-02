using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class DentistView
    {
        public int ID { get; set; }
        
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public double? Salary { get; set; }
        
        public string ImagePath { get; set; }

    }
}