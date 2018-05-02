using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class PrescriptionView
    {
        public int ID { get; set; }
        public DateTime? PrescriptionTime { get; set; }
        public string DentistNameSurname { get; set; }
        public string PatientNameSurname { get; set; }
        public string TreatmentTypeName { get; set; }
        public string TreatmentDescription { get; set; }
        public DateTime? TreatmentTime { get; set; }



    }
}