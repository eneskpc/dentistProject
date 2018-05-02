using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class PatientView
    {
        public string TCNo { get; set; }
        public string Email { get; set; }
        public string NameSurname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
       
        public double? ExchangeRate { get; set; }
        public DateTime? AppointmentDate { get; set; }
    }
}