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
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (Helpers.Login(loginModel.txtUserName, loginModel.txtPassword))
                return RedirectToAction("Index");
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
                return View("TestView");
            }
            else if (pageName == "" && id > 0)
            {

            }
            else if (pageName == "TedaviEkle" && id > 0)
            {

            }
            else if (pageName == "HastaListesi" && id > 0)
            {

            }
            return View();
        }

        public ActionResult Oral()
        {
            return View();
        }
        public ActionResult OralDeneme()
        {
            return View();
        }

        public ActionResult Randevu()
        {
            return View();
        }

        public ActionResult Kullanici(string pageName = "")
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
            if (pageName == "" && id == 0)
            {   //    /Gider
                return View("GiderListele");
            }
            else if (pageName == "Guncelle" && id > 0)
            {
                //Verileri çekip modeli viewa yollicuk.
                //   /Gider/Guncelle/5(giderID)
                return View("GiderGuncelle");
            }
            if (pageName == "Ekle" && id == 0)
            {
                //   /Gider/Ekle
                return View("GiderEkle");
            }
            return View();
        }

        public ActionResult Tedarikci(string pageName = "", int id = 0)
        {
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "" && id == 0)
            {   //    /Tedarikci Listele
                return View("Tedarikci");
            }
            else if (pageName == "Guncelle" && id > 0)
            {
                //Verileri çekip modeli viewa yollicuk.
                //   /Tedarikci/Guncelle/5(giderID)
                return View("TedarikciGuncel");
            }
            if (pageName == "Ekle" && id == 0)
            {
                //   /Tedarikci/Ekle
                return View("TedarikciEkle");
            }
            return View();
        }

        public ActionResult Stok(string pageName = "", int id = 0)
        {
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {

            }
            else if (pageName == "" && id > 0)
            {

            }
            return View();
        }
        public ActionResult StokGuncelle(string pageName = "", int id = 0)
        {
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {

            }
            else if (pageName == "" && id > 0)
            {

            }
            return View();
        }
        public ActionResult StokListele(string pageName = "", int id = 0)
        {
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {

            }
            else if (pageName == "" && id > 0)
            {

            }
            return View();
        }


        public ActionResult Hesap(string pageName = "", int id = 0)
        {
            if (!Helpers.CheckAuthentication())
            {
                return RedirectToAction("Login");
            }
            if (pageName == "Ekle")
            {

            }
            else if (pageName == "" && id > 0)
            {

            }
            return View();
        }
       

    }
}