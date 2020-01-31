using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Stores;
using Nop.Core.Plugins;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Security;
using Nop.Plugin.Widgets.BsProductLocation.Models;
using Nop.Plugin.Widgets.BsProductLocation.Services;
using Nop.Plugin.Widgets.BsProductLocation.Domain;
using Nop.Admin.Extensions;
using Nop.Admin.Controllers;
using Nop.Admin.Models.Catalog;
using Nop.Core.Caching;
using Nop.Core.Domain.Blogs;
using Nop.Services.Shipping;
using Nop.Services.Customers;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services;
using Nop.Services.Common;
using Nop.Services.Events;
using Nop.Services.Seo;
using Nop.Services.Tax;
using Nop.Services.Topics;
using Nop.Web.Models.Catalog;

using Nop.Web.Extensions;
using Nop.Web.Factories;
using Nop.Web.Infrastructure.Cache;


namespace Nop.Plugin.Widgets.BsProductLocation.Controllers
{

    public class ProductLocationController : BasePluginController
    {
        #region Fields
        private readonly IProductLocationRecordService _productLocationRecordService;
       private readonly IPluginFinder _pluginFinder;
        private readonly ILogger _logger;
          private readonly IStoreService _storeService;
        private readonly ProductLocationSettings _productLocationSettings;
        private readonly ISettingService _settingService;
        
        private readonly IShippingService _shippingService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly ICustomerService _customerService;
        private readonly CustomerSettings _customerSettings;
       private readonly IServiceAreaService _serviceAreaService;

      

        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;
        private readonly IVendorService _vendorService;
        private readonly ICategoryTemplateService _categoryTemplateService;
        private readonly IManufacturerTemplateService _manufacturerTemplateService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly ITaxService _taxService;
        private readonly ICurrencyService _currencyService;
        private readonly IPictureService _pictureService;
        private readonly ILocalizationService _localizationService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IWebHelper _webHelper;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IProductTagService _productTagService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ITopicService _topicService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ISearchTermService _searchTermService;
        private readonly IMeasureService _measureService;
        private readonly MediaSettings _mediaSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly VendorSettings _vendorSettings;
        private readonly BlogSettings _blogSettings;
        private readonly ForumSettings _forumSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IProductModelFactory _productModelFactory;

        #endregion

        public ProductLocationController(
            IProductLocationRecordService productLocationRecordService,
            
            IPluginFinder pluginFinder,
            ILogger logger,
            IStoreService storeService,
            ProductLocationSettings productLocationSettings,
            ISettingService settingService,
            IShippingService shippingService,
            IProductAttributeService productAttributeService,
            ICustomerService customerService,
            CustomerSettings customerSettings,
            IServiceAreaService serviceAreaService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IProductService productService,
            IVendorService vendorService,
            ICategoryTemplateService categoryTemplateService,
            IManufacturerTemplateService manufacturerTemplateService,
            IWorkContext workContext,
            IStoreContext storeContext,
            ITaxService taxService,
            ICurrencyService currencyService,
            IPictureService pictureService,
            ILocalizationService localizationService,
            IPriceCalculationService priceCalculationService,
            IPriceFormatter priceFormatter,
            IWebHelper webHelper,
            ISpecificationAttributeService specificationAttributeService,
            IProductTagService productTagService,
            IGenericAttributeService genericAttributeService,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            IPermissionService permissionService,
            ICustomerActivityService customerActivityService,
            ITopicService topicService,
            IEventPublisher eventPublisher,
            ISearchTermService searchTermService,
            IMeasureService measureService,
            MediaSettings mediaSettings,
            CatalogSettings catalogSettings,
            VendorSettings vendorSettings,
            BlogSettings blogSettings,
            ForumSettings forumSettings,
            ICacheManager cacheManager, IProductModelFactory productModelFactory)
        {
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._productService = productService;
            this._vendorService = vendorService;
            this._categoryTemplateService = categoryTemplateService;
            this._manufacturerTemplateService = manufacturerTemplateService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._taxService = taxService;
            this._currencyService = currencyService;
            this._pictureService = pictureService;
            this._localizationService = localizationService;
            this._priceCalculationService = priceCalculationService;
            this._priceFormatter = priceFormatter;
            this._webHelper = webHelper;
            this._specificationAttributeService = specificationAttributeService;
            this._productTagService = productTagService;
            this._genericAttributeService = genericAttributeService;
            this._aclService = aclService;
            this._storeMappingService = storeMappingService;
            this._permissionService = permissionService;
            this._customerActivityService = customerActivityService;
            this._topicService = topicService;
            this._eventPublisher = eventPublisher;
            this._searchTermService = searchTermService;
            this._measureService = measureService;
            this._mediaSettings = mediaSettings;
            this._catalogSettings = catalogSettings;
            this._vendorSettings = vendorSettings;
            this._blogSettings = blogSettings;
            this._forumSettings = forumSettings;
            this._cacheManager = cacheManager;
            _productModelFactory = productModelFactory;
            this._productLocationRecordService = productLocationRecordService;
            this._productService = productService;
            this._currencyService = currencyService;
            this._localizationService = localizationService;
            this._pluginFinder = pluginFinder;
            this._logger = logger;
            this._webHelper = webHelper;
            this._storeService = storeService;
            this._productLocationSettings = productLocationSettings;
            this._settingService = settingService;
            this._permissionService = permissionService;
            this._pictureService = pictureService;

            this._categoryService = categoryService;
            this._vendorService = vendorService;
            this._manufacturerService = manufacturerService;
            this._workContext = workContext;
            this._shippingService = shippingService;
            this._productAttributeService = productAttributeService;
            this._customerService = customerService;
            this._customerSettings = customerSettings;
            _mediaSettings = mediaSettings;
            _serviceAreaService = serviceAreaService;
        }

        #region Utilities
        [NonAction]
        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var categoriesIds = new List<int>();
            var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId, true);
            foreach (var category in categories)
            {
                categoriesIds.Add(category.Id);
                categoriesIds.AddRange(GetChildCategoryIds(category.Id));
            }
            return categoriesIds;
        }
        #endregion
        #region Utilities

        [NonAction]
        protected virtual void PrepareSortingOptions(CatalogPagingFilteringModel pagingFilteringModel, CatalogPagingFilteringModel command)
        {
            if (pagingFilteringModel == null)
                throw new ArgumentNullException("pagingFilteringModel");

            if (command == null)
                throw new ArgumentNullException("command");

            var allDisabled = _catalogSettings.ProductSortingEnumDisabled.Count == Enum.GetValues(typeof(ProductSortingEnum)).Length;
            pagingFilteringModel.AllowProductSorting = _catalogSettings.AllowProductSorting && !allDisabled;

            var activeOptions = Enum.GetValues(typeof(ProductSortingEnum)).Cast<int>()
                .Except(_catalogSettings.ProductSortingEnumDisabled)
                .Select((idOption) =>
                {
                    int order;
                    return new KeyValuePair<int, int>(idOption, _catalogSettings.ProductSortingEnumDisplayOrder.TryGetValue(idOption, out order) ? order : idOption);
                })
                .OrderBy(x => x.Value);
            if (command.OrderBy == null)
                command.OrderBy = allDisabled ? 0 : activeOptions.First().Key;

            if (pagingFilteringModel.AllowProductSorting)
            {
                foreach (var option in activeOptions)
                {
                    var currentPageUrl = _webHelper.GetThisPageUrl(true);
                    var sortUrl = _webHelper.ModifyQueryString(currentPageUrl, "orderby=" + (option.Key).ToString(), null);

                    var sortValue = ((ProductSortingEnum)option.Key).GetLocalizedEnum(_localizationService, _workContext);
                    pagingFilteringModel.AvailableSortOptions.Add(new SelectListItem
                    {
                        Text = sortValue,
                        Value = sortUrl,
                        Selected = option.Key == command.OrderBy
                    });
                }
            }
        }

        [NonAction]
        protected virtual void PrepareViewModes(CatalogPagingFilteringModel pagingFilteringModel, CatalogPagingFilteringModel command)
        {
            if (pagingFilteringModel == null)
                throw new ArgumentNullException("pagingFilteringModel");

            if (command == null)
                throw new ArgumentNullException("command");

            pagingFilteringModel.AllowProductViewModeChanging = _catalogSettings.AllowProductViewModeChanging;

            var viewMode = !string.IsNullOrEmpty(command.ViewMode)
                ? command.ViewMode
                : _catalogSettings.DefaultViewMode;
            pagingFilteringModel.ViewMode = viewMode;
            if (pagingFilteringModel.AllowProductViewModeChanging)
            {
                var currentPageUrl = _webHelper.GetThisPageUrl(true);
                //grid
                pagingFilteringModel.AvailableViewModes.Add(new SelectListItem
                {
                    Text = _localizationService.GetResource("Catalog.ViewMode.Grid"),
                    Value = _webHelper.ModifyQueryString(currentPageUrl, "viewmode=grid", null),
                    Selected = viewMode == "grid"
                });
                //list
                pagingFilteringModel.AvailableViewModes.Add(new SelectListItem
                {
                    Text = _localizationService.GetResource("Catalog.ViewMode.List"),
                    Value = _webHelper.ModifyQueryString(currentPageUrl, "viewmode=list", null),
                    Selected = viewMode == "list"
                });
            }

        }

        [NonAction]
        protected virtual void PreparePageSizeOptions(CatalogPagingFilteringModel pagingFilteringModel, CatalogPagingFilteringModel command,
            bool allowCustomersToSelectPageSize, string pageSizeOptions, int fixedPageSize)
        {
            if (pagingFilteringModel == null)
                throw new ArgumentNullException("pagingFilteringModel");

            if (command == null)
                throw new ArgumentNullException("command");

            if (command.PageNumber <= 0)
            {
                command.PageNumber = 1;
            }
            pagingFilteringModel.AllowCustomersToSelectPageSize = false;
            if (allowCustomersToSelectPageSize && pageSizeOptions != null)
            {
                var pageSizes = pageSizeOptions.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (pageSizes.Any())
                {
                    // get the first page size entry to use as the default (category page load) or if customer enters invalid value via query string
                    if (command.PageSize <= 0 || !pageSizes.Contains(command.PageSize.ToString()))
                    {
                        int temp;
                        if (int.TryParse(pageSizes.FirstOrDefault(), out temp))
                        {
                            if (temp > 0)
                            {
                                command.PageSize = temp;
                            }
                        }
                    }

                    var currentPageUrl = _webHelper.GetThisPageUrl(true);
                    var sortUrl = _webHelper.ModifyQueryString(currentPageUrl, "pagesize={0}", null);
                    sortUrl = _webHelper.RemoveQueryString(sortUrl, "pagenumber");

                    foreach (var pageSize in pageSizes)
                    {
                        int temp;
                        if (!int.TryParse(pageSize, out temp))
                        {
                            continue;
                        }
                        if (temp <= 0)
                        {
                            continue;
                        }

                        pagingFilteringModel.PageSizeOptions.Add(new SelectListItem
                        {
                            Text = pageSize,
                            Value = String.Format(sortUrl, pageSize),
                            Selected = pageSize.Equals(command.PageSize.ToString(), StringComparison.InvariantCultureIgnoreCase)
                        });
                    }

                    if (pagingFilteringModel.PageSizeOptions.Any())
                    {
                        pagingFilteringModel.PageSizeOptions = pagingFilteringModel.PageSizeOptions.OrderBy(x => int.Parse(x.Text)).ToList();
                        pagingFilteringModel.AllowCustomersToSelectPageSize = true;

                        if (command.PageSize <= 0)
                        {
                            command.PageSize = int.Parse(pagingFilteringModel.PageSizeOptions.FirstOrDefault().Text);
                        }
                    }
                }
            }
            else
            {
                //customer is not allowed to select a page size
                command.PageSize = fixedPageSize;
            }

            //ensure pge size is specified
            if (command.PageSize <= 0)
            {
                command.PageSize = fixedPageSize;
            }
        }

      
        /// <summary>
        /// Prepare category (simple) models
        /// </summary>
        /// <param name="rootCategoryId">Root category identifier</param>
        /// <param name="loadSubCategories">A value indicating whether subcategories should be loaded</param>
        /// <param name="allCategories">All available categories; pass null to load them internally</param>
        /// <returns>Category models</returns>
        [NonAction]
        protected virtual IList<CategorySimpleModel> PrepareCategorySimpleModels(int rootCategoryId,
            bool loadSubCategories = true, IList<Category> allCategories = null)
        {
            var result = new List<CategorySimpleModel>();

            //little hack for performance optimization.
            //we know that this method is used to load top and left menu for categories.
            //it'll load all categories anyway.
            //so there's no need to invoke "GetAllCategoriesByParentCategoryId" multiple times (extra SQL commands) to load childs
            //so we load all categories at once
            //if you don't like this implementation if you can uncomment the line below (old behavior) and comment several next lines (before foreach)
            //var categories = _categoryService.GetAllCategoriesByParentCategoryId(rootCategoryId);
            if (allCategories == null)
            {
                //load categories if null passed
                //we implemeneted it this way for performance optimization - recursive iterations (below)
                //this way all categories are loaded only once
                allCategories = _categoryService.GetAllCategories(storeId: _storeContext.CurrentStore.Id);
            }
            var categories = allCategories.Where(c => c.ParentCategoryId == rootCategoryId).ToList();
            foreach (var category in categories)
            {
                var categoryModel = new CategorySimpleModel
                {
                    Id = category.Id,
                    Name = category.GetLocalized(x => x.Name),
                    SeName = category.GetSeName(),
                    IncludeInTopMenu = category.IncludeInTopMenu
                };

                //nubmer of products in each category
                if (_catalogSettings.ShowCategoryProductNumber)
                {
                    string cacheKey = string.Format(ModelCacheEventConsumer.CATEGORY_NUMBER_OF_PRODUCTS_MODEL_KEY,
                        string.Join(",", _workContext.CurrentCustomer.GetCustomerRoleIds()),
                        _storeContext.CurrentStore.Id,
                        category.Id);
                    categoryModel.NumberOfProducts = _cacheManager.Get(cacheKey, () =>
                    {
                        var categoryIds = new List<int>();
                        categoryIds.Add(category.Id);
                        //include subcategories
                        if (_catalogSettings.ShowCategoryProductNumberIncludingSubcategories)
                            categoryIds.AddRange(GetChildCategoryIds(category.Id));
                        return _productService.GetNumberOfProductsInCategory(categoryIds, _storeContext.CurrentStore.Id);
                    });
                }

                if (loadSubCategories)
                {
                    var subCategories = PrepareCategorySimpleModels(category.Id, loadSubCategories, allCategories);
                    categoryModel.SubCategories.AddRange(subCategories);
                }
                result.Add(categoryModel);
            }

            return result;
        }

        [NonAction]
        protected virtual IEnumerable<ProductOverviewModel> PrepareProductOverviewModels(IEnumerable<Product> products,
            bool preparePriceModel = true, bool preparePictureModel = true,
            int? productThumbPictureSize = null, bool prepareSpecificationAttributes = false,
            bool forceRedirectionAfterAddingToCart = false)
        {
            return PrepareProductOverviewModels(products, false).ToList(); 
        }

        #endregion
        #region  location create
        [ChildActionOnly]

        public ActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return Content("Access denied");



            var model = new ProductListModel();
            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem { Text = c.GetFormattedBreadCrumb(categories), Value = c.Id.ToString() });

            //manufacturers
            model.AvailableManufacturers.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var m in _manufacturerService.GetAllManufacturers(showHidden: true))
                model.AvailableManufacturers.Add(new SelectListItem { Text = m.Name, Value = m.Id.ToString() });

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //warehouses
            model.AvailableWarehouses.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var wh in _shippingService.GetAllWarehouses())
                model.AvailableWarehouses.Add(new SelectListItem { Text = wh.Name, Value = wh.Id.ToString() });

            //vendors
            model.AvailableVendors.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var v in _vendorService.GetAllVendors(showHidden: true))
                model.AvailableVendors.Add(new SelectListItem { Text = v.Name, Value = v.Id.ToString() });

            //product types
            model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();
            model.AvailableProductTypes.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            //"published" property
            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.All"), Value = "0" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.PublishedOnly"), Value = "1" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.UnpublishedOnly"), Value = "2" });

            //return View(model);

            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/Configure.cshtml", model);
        }



        [AdminAuthorize]
        public ActionResult LocationCreate(int productId = 0)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return Content("Access denied");

            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return Content("Access denied");

            var model = new ProductLocationRecordModel();
            //SeviceArea
            var serviceAreas = _serviceAreaService.GetServiceAreas();
            model.AvailableServiceAreas.Add(new SelectListItem { Text = "---", Value = "0" });
            foreach (var sa in serviceAreas)
                model.AvailableServiceAreas.Add(new SelectListItem { Text = sa.Name, Value = sa.Id.ToString() });

            model.ProductId = productId;

            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/LocationCreate.cshtml", model);
        }
        [AdminAuthorize]
        [HttpPost]
        [FormValueRequired("save")]
        public ActionResult LocationCreate(ProductLocationRecordModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return Content("Access denied");

            if (!ModelState.IsValid)
            {
                //SeviceArea
                var serviceAreas = _serviceAreaService.GetServiceAreas();
                model.AvailableServiceAreas.Add(new SelectListItem { Text = "---", Value = "0" });
                foreach (var sa in serviceAreas)
                    model.AvailableServiceAreas.Add(new SelectListItem { Text = sa.Name, Value = sa.Id.ToString() });

                return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/LocationCreate.cshtml", model);
            }

            var productLocationRecord = new ProductLocationRecord
            {
                ProductId = model.ProductId,
                ServiceAreaId = model.ServiceAreaId,
                Longitude = model.Longitude,
                Latitude = model.Latitude


            };
            _productLocationRecordService.InsertProductLocationRecord(productLocationRecord);

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return RedirectToAction("LocationCreate");
            //redisplay the form
            //   return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/LocationCreate.cshtml", model);
        }

        [HttpPost]

        [AdminAntiForgery]
        public ActionResult ProductLocationRecordList(DataSourceRequest command, int productId = 0)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return Content("Access denied");

            var productLocationRecords = _productLocationRecordService.GetProductLocationRecords(pageIndex: command.Page - 1,
                pageSize: command.PageSize, productId: productId);
            var productsModel = productLocationRecords
                .Select(x =>
                {
                    var seviceArea = _serviceAreaService.GetServiceAreaById(x.ServiceAreaId);
                    var model = new
                    {
                        Id = x.Id,
                        ProductId = x.ProductId,
                        Longitude = x.Longitude,
                        Latitude = x.Latitude,
                        ServiceAreaId = x.ServiceAreaId,
                        ServiceAreaName = seviceArea != null ? seviceArea.Name : String.Empty,


                    };

                    return model;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = productsModel,
                Total = productLocationRecords.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost]

        [AdminAntiForgery]
        public ActionResult ProductLocationRecordUpdate(ProductLocationRecordModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManagePlugins))
                return Content("Access denied");

            var productLocationRecord = _productLocationRecordService.GetById(model.Id);
            if (productLocationRecord != null)
            {
                productLocationRecord.Longitude = model.Longitude;
                productLocationRecord.Latitude = model.Latitude;
                _productLocationRecordService.UpdateProductLocationRecord(productLocationRecord);

            }

            return new NullJsonResult();
        }

        [HttpPost]

        public ActionResult ProductLocationRecordDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied"); ;

            var productLocationRecord = _productLocationRecordService.GetById(id);
            if (productLocationRecord == null)
                throw new ArgumentException("No record found with the specified id");

            _productLocationRecordService.DeleteProductLocationRecord(productLocationRecord);


            return new NullJsonResult();
        }
        #endregion

        #region Product List

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied");

            var model = new ProductListModel();
            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem { Text = c.GetFormattedBreadCrumb(categories), Value = c.Id.ToString() });

            //manufacturers
            model.AvailableManufacturers.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var m in _manufacturerService.GetAllManufacturers(showHidden: true))
                model.AvailableManufacturers.Add(new SelectListItem { Text = m.Name, Value = m.Id.ToString() });

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //warehouses
            model.AvailableWarehouses.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var wh in _shippingService.GetAllWarehouses())
                model.AvailableWarehouses.Add(new SelectListItem { Text = wh.Name, Value = wh.Id.ToString() });

            //vendors
            model.AvailableVendors.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var v in _vendorService.GetAllVendors(showHidden: true))
                model.AvailableVendors.Add(new SelectListItem { Text = v.Name, Value = v.Id.ToString() });

            //product types
            model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();
            model.AvailableProductTypes.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            //"published" property
            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.All"), Value = "0" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.PublishedOnly"), Value = "1" });
            model.AvailablePublishedOptions.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.UnpublishedOnly"), Value = "2" });

            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/List.cshtml", model);
        }

        [HttpPost]
        public ActionResult ProductList(DataSourceRequest command, ProductListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                model.SearchVendorId = _workContext.CurrentVendor.Id;
            }

            var categoryIds = new List<int> { model.SearchCategoryId };
            //include subcategories
            if (model.SearchIncludeSubCategories && model.SearchCategoryId > 0)
                categoryIds.AddRange(GetChildCategoryIds(model.SearchCategoryId));

            //0 - all (according to "ShowHidden" parameter)
            //1 - published only
            //2 - unpublished only
            bool? overridePublished = null;
            if (model.SearchPublishedId == 1)
                overridePublished = true;
            else if (model.SearchPublishedId == 2)
                overridePublished = false;

            var products = _productService.SearchProducts(
                categoryIds: categoryIds,
                manufacturerId: model.SearchManufacturerId,
                storeId: model.SearchStoreId,
                vendorId: model.SearchVendorId,
                warehouseId: model.SearchWarehouseId,
                productType: model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
                keywords: model.SearchProductName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                showHidden: true,
                overridePublished: overridePublished
            );
            var gridModel = new DataSourceResult();
            gridModel.Data = products.Select(x =>
            {
                var productModel = x.ToModel();
                //little hack here:
                //ensure that product full descriptions are not returned
                //otherwise, we can get the following error if products have too long descriptions:
                //"Error during serialization or deserialization using the JSON JavaScriptSerializer. The length of the string exceeds the value set on the maxJsonLength property. "
                //also it improves performance
                productModel.FullDescription = "";

                productModel.DownloadExpirationDays = _productLocationRecordService.GetByProductId(x.Id).Count();

                //picture
                var defaultProductPicture = _pictureService.GetPicturesByProductId(x.Id, 1).FirstOrDefault();
                productModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                //product type
                productModel.ProductTypeName = x.ProductType.GetLocalizedEnum(_localizationService, _workContext);
                //friendly stock qantity
                //if a simple product AND "manage inventory" is "Track inventory", then display
                if (x.ProductType == ProductType.SimpleProduct && x.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                    productModel.StockQuantityStr = x.GetTotalStockQuantity().ToString();

                return productModel;
            });
            gridModel.Total = products.TotalCount;

            return Json(gridModel);
        }


        #endregion

        #region service Area

        public ActionResult ServiceAreaCreate()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied");

            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/ServiceAreaCreate.cshtml", new ServiceAreaModel());
        }
        [HttpPost]
        public ActionResult ServiceAreaCreate(ServiceAreaModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied");

            if (!model.Name.IsEmpty())
            {
                var serviceArea = new ServiceAreaRecord()
                {
                    Name = model.Name
                };
                _serviceAreaService.InsertServiceArea(serviceArea);
                SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

                return RedirectToAction("ServiceAreaList");
            }

            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/ServiceAreaCreate.cshtml", model);
        }

        public ActionResult ServiceAreaEdit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied");

            var serviceArea = _serviceAreaService.GetServiceAreaById(id);
            if (serviceArea == null)
            {
                return RedirectToAction("ServiceAreaList");

            }

            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/ServiceAreaEdit.cshtml", new ServiceAreaModel() { Id = serviceArea.Id, Name = serviceArea.Name });
        }
        [HttpPost]
        public ActionResult ServiceAreaEdit(ServiceAreaModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied");

            var serviceArea = _serviceAreaService.GetServiceAreaById(model.Id);
            if (model.Name.IsEmpty() || serviceArea == null)
                return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/ServiceAreaEdit.cshtml",
                    model);
            try
            {
                serviceArea.Name = model.Name;
                _serviceAreaService.UpdateServiceArea(serviceArea);
                return RedirectToAction("ServiceAreaList");
            }
            catch (Exception ex)
            {
                // ignored
            }
            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/ServiceAreaEdit.cshtml",
                    model);
        }
        public ActionResult ServiceAreaList()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied");

            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/ServiceAreaList.cshtml");
        }

        [HttpPost]
        public ActionResult ServiceAreaList(DataSourceRequest command, ProductListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return Content("Access denied");


            var serviceAreas = _serviceAreaService.GetServiceAreas(pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            var gridModel = new DataSourceResult();
            gridModel.Data = serviceAreas.Select(x =>
            {
                var serviceAreaModel = new ServiceAreaModel()
                {
                    Id = x.Id,
                    Name = x.Name
                };

                return serviceAreaModel;
            });
            gridModel.Total = serviceAreas.TotalCount;

            return Json(gridModel);
        }

        #endregion


        #region public part
        //[ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {

            if (widgetZone == "body_start_html_tag_after")
            {
                return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/PublicInfo.RootHead.cshtml");
 
            }

            if (additionalData == null)
            {
                return Content("");
            }
            var productId = (int)additionalData;
            var productLocation = _productLocationRecordService.GetByProductId(productId).FirstOrDefault();
            var product = _productService.GetProductById(productId);
            if (productLocation == null || product == null)
            {
                return Content("");
            }
            if (widgetZone == "productbox_addinfo_before")
            {
                var model = new PublicInfoModel()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductSeName = product.GetSeName()
                };
                return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/PublicInfo.cshtml", model);


            }
            if (widgetZone == "productdetails_before_collateral")
            {
                var seviceArea = _serviceAreaService.GetServiceAreaById(productLocation.ServiceAreaId);

                var model = new ProductLocationRecordModel()
                {
                    Longitude = productLocation.Longitude,
                    Latitude = productLocation.Latitude,
                    ProductId = productLocation.ProductId,
                    ServiceAreaId = productLocation.ServiceAreaId,
                    ServiceAreaName = seviceArea != null ? seviceArea.Name : String.Empty,


                };
                //SeviceAreas
                var serviceAreas = _serviceAreaService.GetServiceAreas();
                if (serviceAreas.Count <= 0)
                    return
                        View(
                            "~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/PublicInfoDisplayLocationOnGoogleMap.cshtml",
                            model);
                var storeLocation = _webHelper.GetStoreLocation();
                model.AvailableServiceAreas.Add(new SelectListItem
                {
                    Text = "---",
                    Value = storeLocation + "productsbysa/" + "0"
                });
                foreach (var sa in serviceAreas)
                    model.AvailableServiceAreas.Add(new SelectListItem
                    {
                        Text = sa.Name,
                        Value = storeLocation + "productsbysa/" + sa.Id.ToString()
                    });
                return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/PublicInfoDisplayLocationOnGoogleMap.cshtml", model);

            }
            return Content("");
        }


        #region filter
        protected virtual ActionResult InvokeHttp404()
        {
            // Call target Controller and pass the routeData.
            IController errorController = EngineContext.Current.Resolve<CommonController>();

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Common");
            routeData.Values.Add("action", "PageNotFound");

            errorController.Execute(new RequestContext(this.HttpContext, routeData));

            return new EmptyResult();
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult ProductsByServiceArea(int id, SearchModel model, CatalogPagingFilteringModel command)
        {
            var serviceArea = _serviceAreaService.GetServiceAreaById(id);
            if (serviceArea == null)
                return InvokeHttp404();
            //'Continue shopping' URL
            _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer,
                SystemCustomerAttributeNames.LastContinueShoppingPage,
                _webHelper.GetThisPageUrl(false),
                _storeContext.CurrentStore.Id);

            if (model == null)
                model = new SearchModel();
            var productLocations = _productLocationRecordService.GetProductLocationRecords(serviceAreaId:serviceArea.Id);

            //load products
            var productList = _productService.GetProductsByIds(productLocations.Select(x => x.ProductId).ToArray());
            //ACL and store mapping
            productList = productList.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
            //availability dates
            productList = productList.Where(p => p.IsAvailable()).ToList();



              //view mode
            PrepareViewModes(model.PagingFilteringContext, command);
            //page size
            PreparePageSizeOptions(model.PagingFilteringContext, command,
                _catalogSettings.ProductsByTagAllowCustomersToSelectPageSize,
                _catalogSettings.ProductsByTagPageSizeOptions,
                _catalogSettings.ProductsByTagPageSize);


            IPagedList<Product> products = new PagedList<Product>(productList, pageIndex: command.PageNumber - 1,
                        pageSize: command.PageSize);
            // only search if query string search keyword is set (used to avoid searching or displaying search term min length error message on /search page load)
          
            model.Products = PrepareProductOverviewModels(productList).ToList();

            model.PagingFilteringContext.LoadPagedList(products);

            var customModel = new CustomSearchModel()
            {
                SearchModel = model,
            };
            //SeviceAreas
            var serviceAreas = _serviceAreaService.GetServiceAreas();
            var storeLocation= _webHelper.GetStoreLocation();
            customModel.AvailableServiceAreas.Add(new SelectListItem { Text = "---", Value = storeLocation + "productsbysa/" + "0" });
            foreach (var sa in serviceAreas)
                customModel.AvailableServiceAreas.Add(new SelectListItem { Text = sa.Name, Value = storeLocation + "productsbysa/" + sa.Id.ToString() });


            return View("~/Plugins/Widgets.BsProductLocation/Views/ProductLocation/ProductsByServiceArea.cshtml", customModel);
        }

        #endregion
        #endregion
    }
}
