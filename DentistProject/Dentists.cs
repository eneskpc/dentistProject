//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DentistProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Dentists
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public Nullable<double> Salary { get; set; }
        public Nullable<int> PhotoID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
