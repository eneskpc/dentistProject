using DentistProject.Classes;
using DentistProject.Helpers;
using DentistProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;

namespace DentistProject.Controllers
{
    public class WebServicesController : Controller
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetAppointments(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var appointments = (from a in db.Appointments
                                    join p in db.Patients on a.PatientID equals p.TCNo
                                    join bg in db.BloodGroups on p.BloodGroupID equals bg.ID into pbg
                                    from bg in pbg.DefaultIfEmpty()
                                    join c in db.Currencies on p.CurrencyID equals c.ID into cp
                                    from c in cp.DefaultIfEmpty()
                                    where a.IsDeleted == false || a.IsDeleted == null
                                    where p.IsDeleted == false || p.IsDeleted == null
                                    where bg.IsDeleted == false || bg.IsDeleted == null
                                    where c.IsDeleted == false || c.IsDeleted == null
                                    select new AppointmentView
                                    {
                                        ID = a.ID,
                                        TCNo = a.PatientID,
                                        Email = p.Email,
                                        NameSurname = p.Name + " " + p.Surname,
                                        BirthDate = p.BirthDate,
                                        Telephone = p.Telephone,
                                        Address = p.Address,
                                        BloodGroup = bg.GroupName,
                                        Gender = (p.Gender == "E" ? "Bay" : "Bayan"),
                                        Currency = c.CurrencyName,
                                        ExchangeRate = c.ExchangeRate,
                                        AppointmentDate = a.AppointmentDate,
                                    }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    appointments = appointments.Where(
                        x => x.TCNo.Contains(plainText) ||
                        x.NameSurname.Contains(plainText) ||
                        x.Telephone.Contains(plainText) ||
                        x.Email.Contains(plainText) ||
                        x.BloodGroup.Contains(plainText)
                    );
                }
                return Json(appointments.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddAppointment(string PatientID, string AppointmentDate)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                string reelID = PatientID;
                if (string.IsNullOrEmpty(PatientID) || PatientID == "0")
                {
                    var patient = (from p in db.Patients
                                   where p.IsDeleted == false || p.IsDeleted == null
                                   select p).OrderByDescending(p => p.CreateDate).FirstOrDefault();
                    reelID = patient.TCNo;
                }
                db.Appointments.Add(new Appointments()
                {
                    PatientID = reelID,
                    AppointmentDate = Convert.ToDateTime(AppointmentDate),
                    IsDeleted = false
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteAppointment(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Appointments app = (from a in db.Appointments
                                    where a.IsDeleted == false || a.IsDeleted == null
                                    where a.ID == id
                                    select a).FirstOrDefault();
                app.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateAppointment(int id, string PatientID, string AppointmentDate)
        {
            using (DBEntities db = new DBEntities())
            {
                Appointments app = (from a in db.Appointments
                                    where a.IsDeleted == false || a.IsDeleted == null
                                    where a.ID == id
                                    select a).FirstOrDefault();
                app.PatientID = PatientID;
                app.AppointmentDate = Convert.ToDateTime(AppointmentDate);
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetAssistants(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var assistants = (from a in db.Assistants

                                  where a.IsDeleted == false || a.IsDeleted == null
                                  select new AssistantsView
                                  {
                                      ID = a.ID,
                                      NameSurname = a.Name + " " + a.Surname,
                                      Email = a.Email,
                                      Telephone = a.Telephone,
                                      Address = a.Address,
                                      Salary = a.Salary,
                                  }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    assistants = assistants.Where(
                        x => x.NameSurname.Contains(plainText) ||
                        x.Telephone.Contains(plainText) ||
                        x.Email.Contains(plainText) ||
                         x.Address.Contains(plainText)
                    );
                }
                return Json(assistants.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddAssistant(string Name, string Surname, string Email, string Telephone, string Address, double Salary)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Assistants.Add(new Assistants()
                {
                    Name = Name,
                    Surname = Surname,
                    Email = Email,
                    Telephone = Telephone,
                    Address = Address,
                    Salary = Salary
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteAssistant(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Assistants ass = (from a in db.Assistants
                                  where a.IsDeleted == false || a.IsDeleted == null
                                  where a.ID == id
                                  select a).FirstOrDefault();
                ass.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateAssistant(int id, string Name, string Surname, string Email, string Telephone, string Address, double Salary)
        {
            using (DBEntities db = new DBEntities())
            {
                Assistants ass = (from a in db.Assistants
                                  where a.IsDeleted == false || a.IsDeleted == null
                                  where a.ID == id
                                  select a).FirstOrDefault();
                ass.Name = Name;
                ass.Surname = Surname;
                ass.Email = Email;
                ass.Telephone = Telephone;
                ass.Address = Address;
                ass.Salary = Salary;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetBloodGroups(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var bloodgroups = (from bg in db.BloodGroups
                                   from p in db.Patients
                                   where bg.IsDeleted == false || bg.IsDeleted == null
                                   where p.IsDeleted == false || p.IsDeleted == null
                                   where bg.ID == p.BloodGroupID
                                   select new BloodGroupsView
                                   {
                                       ID = bg.ID,
                                       BloudGroupName = bg.GroupName,

                                   }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    bloodgroups = bloodgroups.Where(
                        x => x.BloudGroupName.Contains(plainText)
                    );
                }
                return Json(bloodgroups.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetCountries(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var countries = (from c in db.Countries
                                 from p in db.Patients
                                 where p.IsDeleted == false || p.IsDeleted == null
                                 where c.IsDeleted == false || c.IsDeleted == null
                                 //where c.ID== p.CountryID
                                 select new CountryView
                                 {
                                     ID = c.ID,
                                     CountryName = c.CountryName,

                                 }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    countries = countries.Where(
                        x => x.CountryName.Contains(plainText)
                    );
                }
                return Json(countries.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetCurrencies(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var currencies = (from c in db.Currencies
                                  from p in db.Patients
                                  where p.IsDeleted == false || p.IsDeleted == null
                                  where c.IsDeleted == false || c.IsDeleted == null
                                  where c.ID == p.CurrencyID
                                  select new CurrenciesView
                                  {
                                      ID = c.ID,
                                      CurrencyName = c.CurrencyName,
                                      ExchangeRate = c.ExchangeRate,
                                  }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    currencies = currencies.Where(
                        x => x.CurrencyName.Contains(plainText)
                    );
                }
                return Json(currencies.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetDentists(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var dentist = (from d in db.Dentists
                               from i in db.Images
                               where d.IsDeleted == false || d.IsDeleted == null
                               where i.IsDeleted == false || i.IsDeleted == null

                               where d.PhotoID == i.ID
                               select new DentistView
                               {
                                   ID = d.ID,
                                   NameSurname = d.Name + " " + d.Surname,
                                   Email = d.Email,
                                   Telephone = d.Telephone,
                                   Address = d.Address,
                                   Salary = d.Salary,
                                   ImagePath = i.ImagePath,

                               }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    dentist = dentist.Where(
                        x => x.NameSurname.Contains(plainText) ||
                        x.Telephone.Contains(plainText) ||
                        x.Email.Contains(plainText) ||
                         x.Address.Contains(plainText)
                    );
                }
                return Json(dentist.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetDentist(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var dentist = (from d in db.Dentists
                               from i in db.Images
                               where d.IsDeleted == false || d.IsDeleted == null
                               where i.IsDeleted == false || i.IsDeleted == null
                               where d.ID == id
                               where d.PhotoID == i.ID
                               select new DentistView
                               {
                                   ID = d.ID,
                                   NameSurname = d.Name + " " + d.Surname,
                                   Email = d.Email,
                                   Telephone = d.Telephone,
                                   Address = d.Address,
                                   Salary = d.Salary,
                                   ImagePath = i.ImagePath,

                               }).Distinct();
                return Json(dentist.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddDentist(string Name, string Surname, string Email, string Telephone, string Address, double Salary, int PhotoID)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Dentists.Add(new Dentists()
                {
                    Name = Name,
                    Surname = Surname,
                    Email = Email,
                    Telephone = Telephone,
                    Address = Address,
                    Salary = Salary,
                    PhotoID = PhotoID
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteDentist(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Dentists dent = (from a in db.Dentists
                                 where a.IsDeleted == false || a.IsDeleted == null
                                 where a.ID == id
                                 select a).FirstOrDefault();
                dent.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateDentist(int id, string Name, string Surname, string Email, string Telephone, string Address, double Salary, int PhotoID)
        {
            using (DBEntities db = new DBEntities())
            {
                Dentists dent = (from a in db.Dentists
                                 where a.IsDeleted == false || a.IsDeleted == null
                                 where a.ID == id
                                 select a).FirstOrDefault();
                dent.Name = Name;
                dent.Surname = Surname;
                dent.Email = Email;
                dent.Telephone = Telephone;
                dent.Address = Address;
                dent.Salary = Salary;
                dent.PhotoID = PhotoID;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetExpenses(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var expenses = (from e in db.Expenses
                                from et in db.ExpenseTypes
                                from bg in db.BloodGroups
                                from c in db.Currencies
                                where e.IsDeleted == false || e.IsDeleted == null
                                where et.IsDeleted == false || et.IsDeleted == null
                                where e.ExpenseType == et.ID
                                select new ExpensesView
                                {
                                    ID = e.ID,
                                    ExpenseName = et.ExpenseName,
                                    ExpenseDescription = e.ExpenseDescription,
                                    Payment = e.Payment,
                                    PaymentDate = e.PaymentDate,

                                }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    expenses = expenses.Where(
                        x => x.ExpenseName.Contains(plainText) ||
                        x.ExpenseDescription.Contains(plainText)

                    );
                }
                return Json(expenses.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetExpense(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var expenses = (from e in db.Expenses
                                from et in db.ExpenseTypes
                                from bg in db.BloodGroups
                                from c in db.Currencies
                                where e.IsDeleted == false || e.IsDeleted == null
                                where et.IsDeleted == false || et.IsDeleted == null
                                where e.ExpenseType == et.ID
                                where e.ID == id
                                select new ExpensesView
                                {
                                    ID = e.ID,
                                    ExpenseName = et.ExpenseName,
                                    ExpenseDescription = e.ExpenseDescription,
                                    Payment = e.Payment,
                                    PaymentDate = e.PaymentDate
                                }).Distinct();
                return Json(expenses.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddExpenses(int ExpenseType, string ExpenseDescription, double Payment, string PaymentDate)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Expenses.Add(new Expenses()
                {
                    ExpenseType = ExpenseType,
                    ExpenseDescription = ExpenseDescription,
                    Payment = Payment,
                    PaymentDate = Convert.ToDateTime(PaymentDate)

                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteExpenses(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Expenses exp = (from a in db.Expenses
                                where a.IsDeleted == false || a.IsDeleted == null
                                where a.ID == id
                                select a).FirstOrDefault();
                exp.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateExpenses(int id, int ExpenseType, string ExpenseDescription, double Payment, string PaymentDate)
        {
            using (DBEntities db = new DBEntities())
            {
                Expenses exp = (from a in db.Expenses
                                where a.IsDeleted == false || a.IsDeleted == null
                                where a.ID == id
                                select a).FirstOrDefault();
                exp.ExpenseType = ExpenseType;
                exp.ExpenseDescription = ExpenseDescription;
                exp.Payment = Payment;
                exp.PaymentDate = Convert.ToDateTime(PaymentDate);
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetExpenseTypes()
        {
            using (DBEntities db = new DBEntities())
            {
                var expensetype = (from et in db.ExpenseTypes
                                   where et.IsDeleted == false || et.IsDeleted == null
                                   select new ExpenseTypeView
                                   {
                                       ID = et.ID,
                                       ExpenseName = et.ExpenseName,
                                   });
                return Json(expensetype.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetImages()
        {
            using (DBEntities db = new DBEntities())
            {
                var images = (from i in db.Images

                              where i.IsDeleted == false || i.IsDeleted == null



                              select new ImageView
                              {
                                  ID = i.ID,
                                  ImagePath = i.ImagePath,
                              });
                return Json(images.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteImages(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Images img = (from a in db.Images
                              where a.IsDeleted == false || a.IsDeleted == null
                              where a.ID == id
                              select a).FirstOrDefault();
                img.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateImages(int id, string ImagePath)
        {
            using (DBEntities db = new DBEntities())
            {
                Images img = (from a in db.Images
                              where a.IsDeleted == false || a.IsDeleted == null
                              where a.ID == id
                              select a).FirstOrDefault();
                img.ImagePath = ImagePath;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddImage(string ImagePath)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Images.Add(new Images()
                {
                    ImagePath = ImagePath
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetMaterialTypes()
        {
            using (DBEntities db = new DBEntities())
            {
                var materialtypes = (from mt in db.MaterialTypes
                                     from sm in db.SupplierMaterials
                                     from s in db.Suppliers

                                     where mt.IsDeleted == false || mt.IsDeleted == null
                                     where sm.IsDeleted == false || sm.IsDeleted == null
                                     where s.IsDeleted == false || s.IsDeleted == null

                                     where mt.ID == sm.MaterialTypeID

                                     select new MaterialTypeView
                                     {
                                         ID = mt.ID,
                                         MaterialTypeName = mt.MaterialTypeName,

                                     });
                return Json(materialtypes.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetMedicines(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var medicines = (from m in db.Medicines

                                 where m.IsDeleted == false || m.IsDeleted == null
                                 select new MedicineView
                                 {
                                     ID = m.ID,
                                     MedicineName = m.MedicineName,
                                     Dosage = m.Dosage,
                                     Usage = m.Usage,
                                     Description = m.Description,
                                 }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    medicines = medicines.Where(
                        x => x.MedicineName.Contains(plainText) ||
                        x.Dosage.Contains(plainText) ||
                        x.Description.Contains(plainText)

                    );
                }
                return Json(medicines.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddMedicine(string MedicineName, string Dosage, string Usage, string Description)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Medicines.Add(new Medicines()
                {
                    MedicineName = MedicineName,
                    Dosage = Dosage,
                    Usage = Usage,
                    Description = Description
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteMedicines(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Medicines med = (from a in db.Medicines
                                 where a.IsDeleted == false || a.IsDeleted == null
                                 where a.ID == id
                                 select a).FirstOrDefault();
                med.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateMedicines(int id, string MedicineName, string Dosage, string Usage, string Description)
        {
            using (DBEntities db = new DBEntities())
            {
                Medicines med = (from a in db.Medicines
                                 where a.IsDeleted == false || a.IsDeleted == null
                                 where a.ID == id
                                 select a).FirstOrDefault();
                med.MedicineName = MedicineName;
                med.Dosage = Dosage;
                med.Usage = Usage;
                med.Description = Description;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetPrescriptionMedicines(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var prescriptionmedicines = (from pm in db.PrescriptionMedicines
                                             from m in db.Medicines
                                             from p in db.Prescriptions
                                             where m.IsDeleted == false || m.IsDeleted == null
                                             where pm.IsDeleted == false || pm.IsDeleted == null
                                             where p.IsDeleted == false || p.IsDeleted == null

                                             where pm.MedicineID == m.ID
                                             where pm.PrescriptionID == p.ID

                                             select new PrescriptionMedicinesView
                                             {
                                                 ID = m.ID,
                                                 MedicineName = m.MedicineName,
                                                 Dosage = m.Dosage,
                                                 Usage = m.Usage,
                                                 Description = m.Description,
                                             }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    prescriptionmedicines = prescriptionmedicines.Where(
                        x => x.MedicineName.Contains(plainText) ||
                        x.Dosage.Contains(plainText) ||
                        x.Usage.Contains(plainText) ||
                         x.Description.Contains(plainText)
                    );
                }
                return Json(prescriptionmedicines.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddPrescriptionMedicine(int MedicineID, int PrescriptionID, int Quantity)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.PrescriptionMedicines.Add(new PrescriptionMedicines()
                {
                    MedicineID = MedicineID,
                    PrescriptionID = PrescriptionID,
                    Quantity = Quantity
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeletePrescriptionMedicine(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                PrescriptionMedicines pre = (from a in db.PrescriptionMedicines
                                             where a.IsDeleted == false || a.IsDeleted == null
                                             where a.ID == id
                                             select a).FirstOrDefault();
                pre.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdatePrescriptionMedicine(int id, int MedicineID, int PrescriptionID, int Quantity)
        {
            using (DBEntities db = new DBEntities())
            {
                PrescriptionMedicines pre = (from a in db.PrescriptionMedicines
                                             where a.IsDeleted == false || a.IsDeleted == null
                                             where a.ID == id
                                             select a).FirstOrDefault();
                pre.MedicineID = MedicineID;
                pre.PrescriptionID = PrescriptionID;
                pre.Quantity = Quantity;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetPrescription(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var prescriptions = (from pr in db.Prescriptions
                                     from d in db.Dentists
                                     from p in db.Patients
                                     where pr.IsDeleted == false || pr.IsDeleted == null
                                     where d.IsDeleted == false || d.IsDeleted == null
                                     where p.IsDeleted == false || p.IsDeleted == null
                                     where pr.PatientID == p.TCNo
                                     select new PrescriptionView
                                     {
                                         ID = pr.ID,
                                         DentistNameSurname = d.Name + " " + d.Surname,
                                         PrescriptionTime = pr.PrescriptionTime,
                                     }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    prescriptions = prescriptions.Where(
                        x => x.DentistNameSurname.Contains(plainText) ||
                        x.TreatmentDescription.Contains(plainText) ||
                        x.PatientNameSurname.Contains(plainText)
                    );
                }
                return Json(prescriptions.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddPrescription(string TcNo, string PrescriptionTime)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Prescriptions.Add(new Prescriptions()
                {
                    PatientID = TcNo,
                    PrescriptionTime = Convert.ToDateTime(PrescriptionTime)
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeletePrescription(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Prescriptions pres = (from a in db.Prescriptions
                                      where a.IsDeleted == false || a.IsDeleted == null
                                      where a.ID == id
                                      select a).FirstOrDefault();
                pres.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdatePrescription(int id, string TCNo, string PrescriptionTime)
        {
            using (DBEntities db = new DBEntities())
            {
                Prescriptions pres = (from a in db.Prescriptions
                                      where a.IsDeleted == false || a.IsDeleted == null
                                      where a.ID == id
                                      select a).FirstOrDefault();
                pres.PatientID = TCNo;
                pres.PrescriptionTime = Convert.ToDateTime(PrescriptionTime);
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetOtherEmployees(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var otheremployees = (from oe in db.OtherEmployees

                                      where oe.IsDeleted == false || oe.IsDeleted == null
                                      select new OtherEmployeeView
                                      {
                                          ID = oe.ID,
                                          NameSurname = oe.Name + " " + oe.Surname,
                                          Telephone = oe.Telephone,
                                          Address = oe.Address,
                                          Salary = oe.Salary,
                                      }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    otheremployees = otheremployees.Where(
                        x => x.NameSurname.Contains(plainText) ||
                        x.Telephone.Contains(plainText) ||
                         x.Address.Contains(plainText)
                    );
                }
                return Json(otheremployees.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddOtherEmployee(string Name, string Surname, string Telephone, string Address, double Salary)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.OtherEmployees.Add(new OtherEmployees()
                {
                    Name = Name,
                    Surname = Surname,
                    Telephone = Telephone,
                    Address = Address,
                    Salary = Salary
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteOtherEmployee(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                OtherEmployees oem = (from a in db.OtherEmployees
                                      where a.IsDeleted == false || a.IsDeleted == null
                                      where a.ID == id
                                      select a).FirstOrDefault();
                oem.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateOtherEmployee(int id, string Name, string Surname, string Telephone, string Address, double Salary)
        {
            using (DBEntities db = new DBEntities())
            {
                OtherEmployees oem = (from a in db.OtherEmployees
                                      where a.IsDeleted == false || a.IsDeleted == null
                                      where a.ID == id
                                      select a).FirstOrDefault();
                oem.Name = Name;
                oem.Surname = Surname;
                oem.Telephone = Telephone;
                oem.Address = Address;
                oem.Salary = Salary;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetPatients(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var patients = (from p in db.Patients
                                join bg in db.BloodGroups on p.BloodGroupID equals bg.ID into pbg
                                from bg in pbg.DefaultIfEmpty()
                                from c in db.Currencies
                                from co in db.Countries
                                where co.IsDeleted == false || co.IsDeleted == null
                                where p.IsDeleted == false || p.IsDeleted == null
                                where bg.IsDeleted == false || bg.IsDeleted == null
                                where c.IsDeleted == false || c.IsDeleted == null
                                where c.ID == p.CurrencyID
                                where co.ID == p.CountryID
                                select new PatientView
                                {
                                    TCNo = p.TCNo,
                                    Email = p.Email,
                                    NameSurname = p.Name + " " + p.Surname,
                                    BirthDate = p.BirthDate,
                                    Telephone = p.Telephone,
                                    Address = p.Address,
                                    BloodGroup = bg.GroupName,
                                    Gender = (p.Gender == "E" ? "Bay" : "Bayan"),
                                    Currency = c.CurrencyName,
                                    Country = co.CountryName,
                                    CreateDate = p.CreateDate
                                });
                if (!string.IsNullOrEmpty(plainText))
                {
                    patients = patients.Where(
                        x => x.TCNo.Contains(plainText) ||
                        x.NameSurname.Contains(plainText) ||
                        x.Telephone.Contains(plainText) ||
                        x.Email.Contains(plainText) ||
                        x.BloodGroup.Contains(plainText)
                    );
                }
                return Json(patients.OrderBy(p => p.CreateDate).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetPatient(string patientID)
        {
            using (DBEntities db = new DBEntities())
            {
                var patients = (from p in db.Patients
                                from bg in db.BloodGroups
                                from c in db.Currencies
                                from co in db.Countries
                                where co.IsDeleted == false || co.IsDeleted == null
                                where p.IsDeleted == false || p.IsDeleted == null
                                where bg.IsDeleted == false || bg.IsDeleted == null
                                where c.IsDeleted == false || c.IsDeleted == null
                                where p.BloodGroupID == bg.ID
                                where c.ID == p.CurrencyID
                                where co.ID == p.CountryID
                                where p.TCNo == patientID
                                select new PatientView
                                {
                                    TCNo = p.TCNo,
                                    Email = p.Email,
                                    NameSurname = p.Name + " " + p.Surname,
                                    BirthDate = p.BirthDate,
                                    Telephone = p.Telephone,
                                    Address = p.Address,
                                    BloodGroup = bg.GroupName,
                                    Gender = (p.Gender == "E" ? "Bay" : "Bayan"),
                                    Currency = c.CurrencyName,
                                    Country = co.CountryName
                                });
                return Json(patients.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddPatient(string TCNo, string Email, string Name, string Surname, string BirthDate, string Telephone, string Address, int BloodGroupID, string Gender, int CurrencyID, int CountryID)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Patients.Add(new Patients()
                {
                    TCNo = TCNo,
                    Email = Email,
                    Name = Name,
                    Surname = Surname,
                    BirthDate = Convert.ToDateTime(BirthDate),
                    Telephone = Telephone,
                    Address = Address,
                    BloodGroupID = BloodGroupID,
                    Gender = Gender,
                    CurrencyID = CurrencyID,
                    CountryID = CountryID,
                    CreateDate = DateTime.Now,
                    IsDeleted = false
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeletePatient(string id)
        {
            using (DBEntities db = new DBEntities())
            {
                Patients pat = (from a in db.Patients
                                where a.IsDeleted == false || a.IsDeleted == null
                                where a.TCNo == id
                                select a).FirstOrDefault();
                pat.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdatePatient(string TCNo, string Email, string Name, string Surname, string BirthDate, string Telephone, string Address, int BloodGroupID, string Gender, int CurrencyID, int CountryID)
        {
            using (DBEntities db = new DBEntities())
            {
                Patients pat = (from a in db.Patients
                                where a.IsDeleted == false || a.IsDeleted == null
                                where a.TCNo == TCNo
                                select a).FirstOrDefault();
                pat.TCNo = TCNo;
                pat.Email = Email;
                pat.Name = Name;
                pat.Surname = Surname;
                if (!string.IsNullOrEmpty(BirthDate))
                    pat.BirthDate = Convert.ToDateTime(BirthDate);
                pat.Telephone = Telephone;
                pat.Address = Address;
                pat.BloodGroupID = BloodGroupID;
                pat.Gender = Gender;
                pat.CurrencyID = CurrencyID;
                pat.CountryID = CountryID;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetStocks(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var stocks = (from st in db.Stock
                              from sm in db.SupplierMaterials
                              from s in db.Suppliers
                              from mt in db.MaterialTypes
                              where st.IsDeleted == false || st.IsDeleted == null
                              where sm.IsDeleted == false || sm.IsDeleted == null
                              where s.IsDeleted == false || s.IsDeleted == null
                              where mt.IsDeleted == false || mt.IsDeleted == null
                              where st.MaterailID == sm.ID
                              where sm.MaterialTypeID == mt.ID
                              where sm.SupplierID == s.ID
                              select new StockView
                              {
                                  ID = st.ID,
                                  Quantity = st.Quantity,
                                  MaterialID = sm.ID,
                                  MaterialName = sm.MaterialName,
                                  UnitPrice = sm.UnitPrice,
                                  SupplierName = s.SupplierName,
                                  Email = s.Email,
                                  Telephone = s.Telephone,
                                  Address = s.Address,
                                  MaterialTypeName = mt.MaterialTypeName,
                              }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    stocks = stocks.Where(
                        x => x.MaterialName.Contains(plainText) ||
                        x.SupplierName.Contains(plainText) ||
                        x.Telephone.Contains(plainText) ||
                        x.MaterialTypeName.Contains(plainText)

                    );
                }
                return Json(stocks.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetStock(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var stocks = (from st in db.Stock
                              from sm in db.SupplierMaterials
                              from s in db.Suppliers
                              from mt in db.MaterialTypes
                              where st.IsDeleted == false || st.IsDeleted == null
                              where sm.IsDeleted == false || sm.IsDeleted == null
                              where s.IsDeleted == false || s.IsDeleted == null
                              where mt.IsDeleted == false || mt.IsDeleted == null
                              where st.MaterailID == sm.ID
                              where sm.MaterialTypeID == mt.ID
                              where sm.SupplierID == s.ID
                              where st.ID == id
                              select new StockView
                              {
                                  ID = st.ID,
                                  Quantity = st.Quantity,
                                  MaterialID = sm.ID,
                                  MaterialName = sm.MaterialName,
                                  UnitPrice = sm.UnitPrice,
                                  SupplierName = s.SupplierName,
                                  Email = s.Email,
                                  Telephone = s.Telephone,
                                  Address = s.Address,
                                  MaterialTypeName = mt.MaterialTypeName,
                              }).Distinct();
                return Json(stocks.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetStocksWithTotal(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var stocks = (from s in db.Stock
                              join sm in db.SupplierMaterials on s.MaterailID equals sm.ID
                              join mt in db.MaterialTypes on sm.MaterialTypeID equals mt.ID
                              group new { s, sm, mt } by new
                              {
                                  s.MaterailID,
                                  sm.MaterialName,
                                  sm.UnitPrice,
                                  mt.MaterialTypeName
                              } into g
                              select new
                              {
                                  g.Key.MaterailID,
                                  g.Key.MaterialName,
                                  g.Key.UnitPrice,
                                  g.Key.MaterialTypeName,
                                  Quantity = g.Sum(p => p.s.Quantity)
                              }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    stocks = stocks.Where(
                        x => x.MaterialName.Contains(plainText) ||
                        x.MaterialTypeName.Contains(plainText)
                    );
                }
                return Json(stocks.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddStock(int MaterailID, double Quantity)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                var stockMaterial = (from sm in db.Stock
                                     where sm.IsDeleted == false || sm.IsDeleted == null
                                     where sm.MaterailID == MaterailID
                                     select sm).FirstOrDefault();
                if (stockMaterial == null)
                {
                    db.Stock.Add(new Stock()
                    {
                        MaterailID = MaterailID,
                        Quantity = Quantity
                    });
                    isSuccess = (db.SaveChanges() > 0 ? true : false);
                }
                else
                {
                    stockMaterial.Quantity = Quantity;
                    isSuccess = (db.SaveChanges() > 0 ? true : false);
                }
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteStock(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Stock pre = (from a in db.Stock
                             where a.IsDeleted == false || a.IsDeleted == null
                             where a.ID == id
                             select a).FirstOrDefault();
                pre.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateStock(int id, int MaterailID, double Quantity)
        {
            using (DBEntities db = new DBEntities())
            {
                Stock stc = (from a in db.Stock
                             where a.IsDeleted == false || a.IsDeleted == null
                             where a.ID == id
                             select a).FirstOrDefault();

                stc.MaterailID = MaterailID;
                stc.Quantity = Quantity;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetSupplierMaterials(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var suppliermaterials = (from sm in db.SupplierMaterials
                                         from s in db.Suppliers
                                         from mt in db.MaterialTypes
                                         where s.IsDeleted == false || s.IsDeleted == null
                                         where mt.IsDeleted == false || mt.IsDeleted == null
                                         where sm.IsDeleted == false || sm.IsDeleted == null
                                         where sm.MaterialTypeID == mt.ID
                                         where sm.SupplierID == s.ID
                                         select new SupplierMeterialView
                                         {
                                             ID = sm.ID,
                                             MaterialName = sm.MaterialName,
                                             UnitPrice = sm.UnitPrice,
                                             SupplierName = s.SupplierName,
                                             Email = s.Email,
                                             Telephone = s.Telephone,
                                             Address = s.Address,
                                             MaterialTypeName = mt.MaterialTypeName,
                                         }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    suppliermaterials = suppliermaterials.Where(
                        x => x.MaterialName.Contains(plainText) ||
                        x.SupplierName.Contains(plainText) ||
                        x.Telephone.Contains(plainText) ||
                        x.MaterialTypeName.Contains(plainText)
                    );
                }
                return Json(suppliermaterials.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddSupplierMaterial(int SupplierID, int MaterialTypeID, string MaterialName, double UnitPrice)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.SupplierMaterials.Add(new SupplierMaterials()
                {
                    SupplierID = SupplierID,
                    MaterialTypeID = MaterialTypeID,
                    MaterialName = MaterialName,
                    UnitPrice = UnitPrice


                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteSupplierMaterial(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                SupplierMaterials sup = (from a in db.SupplierMaterials
                                         where a.IsDeleted == false || a.IsDeleted == null
                                         where a.ID == id
                                         select a).FirstOrDefault();
                sup.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateSupplierMaterial(int id, int SupplierID, int MaterialTypeID, string MaterialName, double UnitPrice)
        {
            using (DBEntities db = new DBEntities())
            {
                SupplierMaterials sup = (from a in db.SupplierMaterials
                                         where a.IsDeleted == false || a.IsDeleted == null
                                         where a.ID == id
                                         select a).FirstOrDefault();

                sup.SupplierID = SupplierID;
                sup.MaterialTypeID = MaterialTypeID;
                sup.MaterialName = MaterialName;
                sup.UnitPrice = UnitPrice;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetSuppliers(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var suppliers = (from s in db.Suppliers
                                 where s.IsDeleted == false || s.IsDeleted == null
                                 select new SupplierView
                                 {
                                     ID = s.ID,
                                     SupplierName = s.SupplierName,
                                     Email = s.Email,
                                     Telephone = s.Telephone,
                                     Address = s.Address,

                                 }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    suppliers = suppliers.Where(
                        x => x.SupplierName.Contains(plainText) ||
                        x.SupplierName.Contains(plainText) ||
                        x.Telephone.Contains(plainText) ||
                        x.Address.Contains(plainText)

                    );
                }
                return Json(suppliers.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetSupplier(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var suppliers = (from s in db.Suppliers
                                 where s.IsDeleted == false || s.IsDeleted == null
                                 where s.ID == id
                                 select new SupplierView
                                 {
                                     ID = s.ID,
                                     SupplierName = s.SupplierName,
                                     Email = s.Email,
                                     Telephone = s.Telephone,
                                     Address = s.Address,

                                 }).Distinct();
                return Json(suppliers.FirstOrDefault(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddSupplier(string SupplierName, string Address, string Telephone, string Email)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Suppliers.Add(new Suppliers()
                {
                    SupplierName = SupplierName,
                    Address = Address,
                    Telephone = Telephone,
                    Email = Email


                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteSupplier(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Suppliers supp = (from a in db.Suppliers
                                  where a.IsDeleted == false || a.IsDeleted == null
                                  where a.ID == id
                                  select a).FirstOrDefault();
                supp.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateSupplier(int id, string SupplierName, string Address, string Telephone, string Email)
        {
            using (DBEntities db = new DBEntities())
            {
                Suppliers supp = (from a in db.Suppliers
                                  where a.IsDeleted == false || a.IsDeleted == null
                                  where a.ID == id
                                  select a).FirstOrDefault();
                supp.SupplierName = SupplierName;
                supp.Address = Address;
                supp.Telephone = Telephone;
                supp.Email = Email;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetTreatments(string plainText, string patientID)
        {
            using (DBEntities db = new DBEntities())
            {
                var treatments = (from t in db.Treatments
                                  from d in db.Dentists
                                  from p in db.Patients
                                  from tt in db.TreatmentTypes
                                  where t.IsDeleted == false || t.IsDeleted == null
                                  where d.IsDeleted == false || d.IsDeleted == null
                                  where p.IsDeleted == false || p.IsDeleted == null
                                  where tt.IsDeleted == false || tt.IsDeleted == null
                                  where t.PatientID == p.TCNo
                                  where t.TreatmentTypeID == tt.ID
                                  where t.DentistID == d.ID
                                  select new TreatmentView
                                  {
                                      ID = t.ID,
                                      DentistNameSurname = d.Name + " " + d.Surname,
                                      TreatmentTime = t.TreatmentTime,
                                      TreatmentDescription = t.TreatmentDescription,
                                      PatientID = p.TCNo,
                                      PatientNameSurname = p.Name + " " + p.Surname,
                                      TreatmentTypeName = tt.TreatmentTypeName,
                                      ToothNumber = t.ToothNumber,
                                      Price = tt.Price
                                  }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    treatments = treatments.Where(
                        x => x.DentistNameSurname.Contains(plainText) ||
                        x.TreatmentDescription.Contains(plainText) ||
                        x.PatientNameSurname.Contains(plainText) ||
                        x.TreatmentTypeName.Contains(plainText)

                    );
                }
                if (!string.IsNullOrEmpty(patientID))
                {
                    treatments = treatments.Where(x => x.PatientID == patientID);
                }
                return Json(treatments.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddTreatment(int DentistID, string PatientID, int TreatmentTypeID, int ToothNumber, string TreatmentDescription)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Treatments.Add(new Treatments()
                {
                    DentistID = DentistID,
                    PatientID = PatientID,
                    TreatmentTypeID = TreatmentTypeID,
                    ToothNumber = ToothNumber,
                    TreatmentDescription = TreatmentDescription,
                    TreatmentTime = DateTime.Now
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteTreatment(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Treatments tre = (from a in db.Treatments
                                  where a.IsDeleted == false || a.IsDeleted == null
                                  where a.ID == id
                                  select a).FirstOrDefault();
                tre.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateTreatment(int id, int DentistID, string PatientID, int TreatmentTypeID, string TreatmentDescription, string TreatmentTime)
        {
            using (DBEntities db = new DBEntities())
            {
                Treatments tre = (from a in db.Treatments
                                  where a.IsDeleted == false || a.IsDeleted == null
                                  where a.ID == id
                                  select a).FirstOrDefault();
                tre.DentistID = DentistID;
                tre.PatientID = PatientID;
                tre.TreatmentTypeID = TreatmentTypeID;
                tre.TreatmentDescription = TreatmentDescription;
                tre.TreatmentTime = Convert.ToDateTime(TreatmentTime);
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetTreatmentTypes()
        {
            using (DBEntities db = new DBEntities())
            {
                var treatmenttypes = (from tt in db.TreatmentTypes

                                      where tt.IsDeleted == false || tt.IsDeleted == null

                                      select new TreatmentTypeView
                                      {
                                          ID = tt.ID,
                                          TreatmentTypeName = tt.TreatmentTypeName,
                                          Price = tt.Price
                                      });
                return Json(treatmenttypes.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteTreatmentType(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                TreatmentTypes tr = (from a in db.TreatmentTypes
                                     where a.IsDeleted == false || a.IsDeleted == null
                                     where a.ID == id
                                     select a).FirstOrDefault();
                tr.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateTreatmentType(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                TreatmentTypes tr = (from a in db.TreatmentTypes
                                     where a.IsDeleted == false || a.IsDeleted == null
                                     where a.ID == id
                                     select a).FirstOrDefault();
                tr.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult GetUser(string plainText)
        {
            using (DBEntities db = new DBEntities())
            {
                var users = (from u in db.Users

                             where u.IsDeleted == false || u.IsDeleted == null

                             select new UsersView
                             {
                                 ID = u.ID,
                                 UserEmail = u.UserEmail,
                                 Password = u.Password,
                                 CreateDate = u.CreateDate,
                                 UserType = u.UserType


                             }).Distinct();
                if (!string.IsNullOrEmpty(plainText))
                {
                    users = users.Where(
                        x => x.UserEmail.Contains(plainText) ||
                        x.Password.Contains(plainText)

                    );
                }
                return Json(users.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult Login(string userName, string password)
        {
            using (DBEntities db = new DBEntities())
            {
                string encyptedPassword = CryptoHelper.EncrytCustomMD5(password);
                var user = (from u in db.Users
                            where u.IsDeleted == false || u.IsDeleted == null
                            where u.UserEmail == userName
                            where u.Password == encyptedPassword
                            select new UsersView
                            {
                                ID = u.ID,
                                UserEmail = u.UserEmail,
                                Password = u.Password,
                                CreateDate = u.CreateDate,
                                UserType = u.UserType
                            }).FirstOrDefault();
                if (user != null)
                {
                    switch (user.UserType)
                    {
                        case 1://Dentist
                            var doctorInfo = (from ui in db.Dentists
                                              where ui.IsDeleted == false || ui.IsDeleted == null
                                              where ui.Email == userName
                                              select ui).FirstOrDefault();
                            HttpContext.Session["DoctorInfo"] = doctorInfo;
                            return Json(true, JsonRequestBehavior.AllowGet);
                        case 2://Asistant
                            var asistantInfo = (from ui in db.Assistants
                                                where ui.IsDeleted == false || ui.IsDeleted == null
                                                where ui.Email == userName
                                                select ui).FirstOrDefault();
                            HttpContext.Session["AsistantInfo"] = asistantInfo;
                            return Json(true, JsonRequestBehavior.AllowGet);
                        case 3://Patient
                            var patientInfo = (from ui in db.Patients
                                               where ui.IsDeleted == false || ui.IsDeleted == null
                                               where ui.Email == userName
                                               select ui).FirstOrDefault();
                            HttpContext.Session["PatientInfo"] = patientInfo;
                            return Json(true, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult Logout()
        {
            HttpContext.Session["DoctorInfo"] = null;
            HttpContext.Session["AsistantInfo"] = null;
            HttpContext.Session["PatientInfo"] = null;

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult AddUser(string UserEmail, string Password, int UserType, string CreateDate)
        {
            using (DBEntities db = new DBEntities())
            {
                bool isSuccess = false;
                db.Users.Add(new Users()
                {
                    UserEmail = UserEmail,
                    Password = Password,
                    UserType = UserType,
                    CreateDate = Convert.ToDateTime(CreateDate)
                });
                isSuccess = (db.SaveChanges() > 0 ? true : false);
                return Json(isSuccess, JsonRequestBehavior.AllowGet);
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult DeleteUser(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                Users usr = (from a in db.Users
                             where a.IsDeleted == false || a.IsDeleted == null
                             where a.ID == id
                             select a).FirstOrDefault();
                usr.IsDeleted = true;
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public JsonResult UpdateUser(int id, string UserEmail, string Password, int UserType, string CreateDate)
        {
            using (DBEntities db = new DBEntities())
            {
                Users usr = (from a in db.Users
                             where a.IsDeleted == false || a.IsDeleted == null
                             where a.ID == id
                             select a).FirstOrDefault();
                usr.UserEmail = UserEmail;
                usr.Password = Password;
                usr.UserType = UserType;
                usr.CreateDate = Convert.ToDateTime(CreateDate);
                return Json(db.SaveChanges() > 0, JsonRequestBehavior.AllowGet);

            }
        }
    }
}