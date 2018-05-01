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
        public JsonResult GetAppointments()
        {
            using (DBEntities db = new DBEntities())
            {
                var appointments = (from a in db.Appointments
                                    from p in db.Patients
                                    from bg in db.BloodGroups
                                    from c in db.Currencies
                                    where a.IsDeleted == false || a.IsDeleted == null
                                    where p.IsDeleted == false || p.IsDeleted == null
                                    where bg.IsDeleted == false || bg.IsDeleted == null
                                    where c.IsDeleted == false || c.IsDeleted == null
                                    where a.PatientID == p.TCNo
                                    where p.BloodGroupID == bg.ID
                                    where c.ID == p.CurrencyID
                                    select new AppointmentView
                                    {
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
                                    });
                return Json(appointments.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}