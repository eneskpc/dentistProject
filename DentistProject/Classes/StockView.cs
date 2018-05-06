using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class StockView
    {
        public int ID { get; set; }
        public double? Quantity { get; set; }
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public double? UnitPrice { get; set; }
        public string SupplierName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string MaterialTypeName { get; set; }
    }
}