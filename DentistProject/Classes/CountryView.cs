using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class CountryView
    {
        public int ID { get; set; }
        public string CountryName { get; set; }
        public int? PriorityOrder { get; set; }
    }
}