using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Widgets.BsProductLocation.Infrastructure
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {

           
            routes.MapLocalizedRoute("BsProductLocation.ProductList",
                          "product-location/product-list",
                          new { controller = "ProductLocation", action = "List", area = "" },
                          new[] { "Nop.Plugin.Widgets.BsProductLocation.Controllers" });

            routes.MapLocalizedRoute("BsProductLocation.LocationCreate",
                          "product-location/create/{productId}",
                          new { controller = "ProductLocation", action = "LocationCreate", productId = UrlParameter.Optional, area = "" },
                          new[] { "Nop.Plugin.Widgets.BsProductLocation.Controllers" });

            routes.MapLocalizedRoute("BsProductLocation.SearchByServiceArea",
                        "productsbysa/{id}",
                        new { controller = "ProductLocation", action = "ProductsByServiceArea", area = "" },
                        new[] { "Nop.Plugin.Widgets.BsProductLocation.Controllers" });
        
        }
        public int Priority
        {
            get
            {
                return 2;
            }
        }
    }
}
