using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class PrescriptionMedicinesView
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Usage { get; set; }
        public string Description { get; set; }
        public DateTime? PrescriptionTime { get; set; }


    }
}