//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DentistProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expenses
    {
        public int ID { get; set; }
        public Nullable<int> ExpenseType { get; set; }
        public string ExpenseDescription { get; set; }
        public Nullable<double> Payment { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}