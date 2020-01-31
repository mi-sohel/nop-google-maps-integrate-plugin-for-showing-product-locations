using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Routing;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Plugin.Widgets.BsProductLocation.Data;
using Nop.Services.Common;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.BsProductLocation
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class ProductLocationPlugin : BasePlugin, IWidgetPlugin, IAdminMenuPlugin, IMiscPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly ProductLocationObjectContext _objectContext;

        public ProductLocationPlugin(IPictureService pictureService,
            ISettingService settingService, IWebHelper webHelper, ProductLocationObjectContext objectContext)
        {
            this._pictureService = pictureService;
            this._settingService = settingService;
            this._webHelper = webHelper;
            this._objectContext = objectContext;
        }



        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { "body_start_html_tag_after", "productbox_addinfo_before", "productdetails_before_collateral" };
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ProductLocation";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Widgets.BsProductLocation.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Gets a route for displaying widget
        /// </summary>
        /// <param name="widgetZone">Widget zone where it's displayed</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "ProductLocation";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.BsProductLocation.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }
        
        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //save settings
            
            //_settingService.SaveSetting(settings);


            this.AddOrUpdatePluginLocaleResource("Nop.Plugin.Widgets.BsProductLocation.Button.AddLocation", "Add Location/Audio");
            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductLocation.ProductId", "Product Id");

            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductLocation.ProductName", "Product Name");
            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductLocation.EmbedLocationHtmlCode", "Embed Location Audio Html Code");
            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductLocation.Picture", "Thumbnail");
            this.AddOrUpdatePluginLocaleResource("Plugin.Widgets.BsProductLocation.DisplayOrder", "Display Order");
            
               //data
            _objectContext.Install();


            base.Install();


           
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            //_settingService.DeleteSetting<SettingsName>();

            //locales
            this.DeletePluginLocaleResource("key");

            this.DeletePluginLocaleResource("Nop.Plugin.Widgets.BsProductLocation.Button.AddLocation");
            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductLocation.ProductId");

            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductLocation.ProductName");
            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductLocation.EmbedLocationHtmlCode");
            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductLocation.Picture");
            this.DeletePluginLocaleResource("Plugin.Widgets.BsProductLocation.DisplayOrder");
            
           //data
            _objectContext.Uninstall();

            base.Uninstall();
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {

           

            var menuItem = new SiteMapNode()
            {
                Visible = true,
                Title = "Product Location",
                Url = ""
            };

            var menuItemProductList = new SiteMapNode()
            {
                Visible = true,
                Title = "Location",
                Url = "/product-location/product-list",
                RouteValues = new RouteValueDictionary() { { "Area", "Admin" } }
            };

            var menuItemServiceAreaList = new SiteMapNode()
            {
                Visible = true,
                Title = "ServiceArea",
                Url = "/ProductLocation/ServiceAreaList",
                RouteValues = new RouteValueDictionary() { { "Area", "Admin" } }
            };
            menuItem.ChildNodes.Add(menuItemServiceAreaList);
            menuItem.ChildNodes.Add(menuItemProductList);
           

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
            {
                var sohel = new SiteMapNode()
                {
                    Visible = true,
                    Title = "SohelPlugins",
                    Url = "",
                    SystemName = "nopSohel"
                };
                sohel.ChildNodes.Add(menuItem);

                rootNode.ChildNodes.Add(sohel);
            }

           // rootNode.ChildNodes.Add(menuItemBuilder);
            
        }
    }
}
