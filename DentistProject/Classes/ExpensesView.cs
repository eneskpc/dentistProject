using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentistProject.Classes
{
    public class ExpensesView
    {
        public int ID { get; set; }
        public int ExpenseType { get; set; }
        public string ExpenseName { get; set; }
        public string ExpenseDescription { get; set; }
        public double? Payment { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}