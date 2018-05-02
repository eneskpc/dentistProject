using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class CurrenciesView
    {
        public int ID { get; set; }
        public string CurrencyName { get; set; }
        public double? ExchangeRate { get; set; }
    }
}