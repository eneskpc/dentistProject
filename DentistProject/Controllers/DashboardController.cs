using DentistProject.Classes;
using DentistProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DentistProject.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (!Classes.Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Randevu");
        }

        public ActionResult Login()
        {
            if (Classes.Helpers.CheckAuthentication())
            {
                return Redirect("/Dashboard");
            }
            return View();
        }

        public ActionResult HastaKarti(string pageName = "", string id = "0")
        {
            if (!Classes.Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {
                return View("HastaKarti");
            }
            else if (pageName == "Guncelle" && id == "0")
            {
                return View("HastaListesi");
            }
            else if (pageName == "Guncelle" && id != "0")
            {
                Patients thisPatient = null;
                using (DBEntities db = new DBEntities())
                {
                    thisPatient = (from p in db.Patients
                                   where p.IsDeleted == false
                                   select p).FirstOrDefault();
                }
                return View("HastaKarti", thisPatient);
            }
            return View();
        }

        public ActionResult Tedavi(string pageName = "", int id = 0)
        {
            if (!Classes.Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            return View("Oral");
        }
        public ActionResult Randevu()
        {
            if (!Classes.Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Gider(string pageName = "", int id = 0)
        {
            if (!Classes.Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {
                return View("GiderEkle");
            }
            else if (pageName == "Guncelle" && id > 0)
            {
                return View("GiderEkle");
            }
            return View("GiderListele");
        }
        public ActionResult Stok(string pageName = "", int id = 0)
        {
            if (!Classes.Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {
                return View("Stok");
            }
            else if (pageName == "Guncelle" && id > 0)
            {
                return View("Stok");
            }
            return View("StokListele");
        }
        public ActionResult Tedarikci(string pageName = "", int id = 0)
        {
            if (!Classes.Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {
                return View("TedarikciEkle");
            }
            else if (pageName == "Guncelle" && id > 0)
            {
                return View("TedarikciEkle");
            }
            return View("Tedarikci");
        }
        public ActionResult Recete()
        {
            if (!Classes.Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            return View("ReceteEkle");
        }
    }
}