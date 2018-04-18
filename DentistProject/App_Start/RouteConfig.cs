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
                name: "ReceteEklemeRoute",
                /*     classs /  method or function / parameter     */
                url: "Dashboard/Hasta/Recete",
                defaults: new { controller = "Dashboard", action = "HastaKarti", pageName = "Recete", id = 5 }
            );
            routes.MapRoute(
                name: "TedarikciEkleRoute",
                /*     classs /  method or function / parameter     */
                url: "Dashboard/Tedarikci/Ekle",
                defaults: new { controller = "Dashboard", action = "Tedarikci", pageName = "Ekle", id = 0 }
            );
            routes.MapRoute(
                name: "GiderEkleRoute",
                /*     classs /  method or function / parameter     */
                url: "Dashboard/Gider/Ekle",
                defaults: new { controller = "Dashboard", action = "Gider", pageName = "Ekle", id = 0 }
            );
            routes.MapRoute(
                name: "Default",
                /*     classs /  method or function / parameter    */
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Another",
                /*     classs /  method or function / parameter     */
                url: "{controller}/{action}/{pageName}/{id}",
                defaults: new { controller = "Home", action = "Index", pageName = UrlParameter.Optional, id = UrlParameter.Optional }
            );
        }
    }
}
