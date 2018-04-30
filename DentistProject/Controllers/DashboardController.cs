using DentistProject.Classes;
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
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            if (Helpers.CheckAuthentication())
            {
                return Redirect("/Dashboard");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (Helpers.Login(loginModel.txtUserName, loginModel.txtPassword))
                return Redirect("/Dashboard");
            return View();
        }

        public ActionResult HastaKarti(string pageName = "", int id = 0)
        {
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {
                return View("HastaKarti");
            }
            else if (pageName == "Guncelle" && id == 0)
            {
                return View("HastaListesi");
            }
            else if (pageName == "Guncelle" && id > 0)
            {
                return View("HastaKarti");
            }
            return View();
        }

        public ActionResult Tedavi(string pageName = "", int id = 0)
        {
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {
                return View("Oral");
            }
            else if (pageName == "Guncelle" && id == 0)
            {
                return View("HastaListesi");
            }
            return View();
        }
        public ActionResult Randevu()
        {
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Gider(string pageName = "", int id = 0)
        {
            if (!Helpers.CheckAuthentication())
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
            if (!Helpers.CheckAuthentication())
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
            if (!Helpers.CheckAuthentication())
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
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            return View("ReceteEkle");
        }
    }
}