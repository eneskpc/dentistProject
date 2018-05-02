﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Assistants> Assistants { get; set; }
        public virtual DbSet<BloodGroups> BloodGroups { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Currencies> Currencies { get; set; }
        public virtual DbSet<Dentists> Dentists { get; set; }
        public virtual DbSet<Expenses> Expenses { get; set; }
        public virtual DbSet<ExpenseTypes> ExpenseTypes { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<MaterialTypes> MaterialTypes { get; set; }
        public virtual DbSet<Medicines> Medicines { get; set; }
        public virtual DbSet<OtherEmployees> OtherEmployees { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }
        public virtual DbSet<PrescriptionMedicines> PrescriptionMedicines { get; set; }
        public virtual DbSet<Prescriptions> Prescriptions { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<SupplierMaterials> SupplierMaterials { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<Treatments> Treatments { get; set; }
        public virtual DbSet<TreatmentTypes> TreatmentTypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<CountriesWithOrder> CountriesWithOrder { get; set; }
    }
}
