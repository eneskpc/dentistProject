using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class DoctorUser
    {
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public double Salary { get; set; }
        public string PhotoURL { get; set; }
    }
}