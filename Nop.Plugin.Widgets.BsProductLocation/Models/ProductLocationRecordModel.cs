using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.BsProductLocation.Models
{
    public class ProductLocationRecordModel:BaseNopEntityModel
    {
        public ProductLocationRecordModel()
        {
            AvailableServiceAreas=new List<SelectListItem>();
        }
        public int ProductId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int ServiceAreaId { get; set; }

        public string ServiceAreaName { get; set; }
        public IList<SelectListItem> AvailableServiceAreas { get; set; }

       
    }
}