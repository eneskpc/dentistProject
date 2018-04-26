using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DentistProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "TedarikciEkle",
                url: "Dashboard/Tedarikci/Ekle",
                defaults: new { controller = "Dashboard", action = "Tedarikci", pageName = "Ekle" }
            );
            routes.MapRoute(
                name: "TedarikciGuncelle",
                url: "Dashboard/Tedarikci/{id}",
                defaults: new { controller = "Dashboard", action = "Tedarikci", pageName = "Guncelle" }
            );
            routes.MapRoute(
                name: "TedarikciListele",
                url: "Dashboard/Tedarikci",
                defaults: new { controller = "Dashboard", action = "Tedarikci", pageName = "Listele" }
            );
            routes.MapRoute(
                name: "MalzemeEkle",
                url: "Dashboard/Stok/Ekle",
                defaults: new { controller = "Dashboard", action = "Stok", pageName = "Ekle" }
            );
            routes.MapRoute(
                name: "MalzemeGuncelle",
                url: "Dashboard/Stok/{id}",
                defaults: new { controller = "Dashboard", action = "Stok", pageName = "Guncelle", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "MalzemeListele",
                url: "Dashboard/Stok",
                defaults: new { controller = "Dashboard", action = "Stok", pageName = "Listele"}
            );
            routes.MapRoute(
                name: "GiderEkle",
                url: "Dashboard/Gider/Ekle",
                defaults: new { controller = "Dashboard", action = "Gider", pageName = "Ekle" }
            );
            routes.MapRoute(
                name: "GiderGuncelle",
                url: "Dashboard/Gider/{id}",
                defaults: new { controller = "Dashboard", action = "Gider", pageName = "Guncelle", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Giderler",
                url: "Dashboard/Gider",
                defaults: new { controller = "Dashboard", action = "Gider", pageName = "Listele" }
            );
            routes.MapRoute(
                name: "Randevular",
                url: "Dashboard/Randevu",
                defaults: new { controller = "Dashboard", action = "Randevu" }
            );
            routes.MapRoute(
                name: "HastayaTedaviEkle",
                url: "Dashboard/Tedavi/Ekle",
                defaults: new { controller = "Dashboard", action = "Tedavi", pageName = "Ekle", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "HastaEkleme",
                url: "Dashboard/Hasta/Ekle",
                defaults: new { controller = "Dashboard", action = "HastaKarti", pageName = "Ekle", id = 0 }
            );
            routes.MapRoute(
                name: "HastaGuncelleme",
                url: "Dashboard/Hasta/{id}",
                defaults: new { controller = "Dashboard", action = "HastaKarti", pageName = "Guncelle", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Another",
                url: "{controller}/{action}/{pageName}/{id}",
                defaults: new { controller = "Home", action = "Index", pageName = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
