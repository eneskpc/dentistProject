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
                name: "HastayaTedaviEkle",
                url: "Dashboard/Tedavi/Ekle",
                defaults: new { controller = "Dashboard", action = "Tedavi", pageName = "Ekle", id = 0 }
            );
            routes.MapRoute(
                name: "HastaninTedaviyiGuncelle",
                url: "Dashboard/Tedavi/{id}",
                defaults: new { controller = "Dashboard", action = "Tedavi", pageName = "Guncelle", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "HastayaninTedavileri",
                url: "Dashboard/Hasta/{id}/Tedaviler",
                defaults: new { controller = "Dashboard", action = "Tedavi", pageName = "Guncelle", id = UrlParameter.Optional }
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
