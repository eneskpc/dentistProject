using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class TreatmentView
    {
        public int ID { get; set; }
        public string TreatmentDescription { get; set; }
        public DateTime? TreatmentTime { get; set; }
        public string DentistNameSurname { get; set; }
        public string PatientID { get; set; }
        public string PatientNameSurname { get; set; }
        public string TreatmentTypeName { get; set; }
        public int? ToothNumber { get; set; }
        public double? Price { get; set; }
    }
}