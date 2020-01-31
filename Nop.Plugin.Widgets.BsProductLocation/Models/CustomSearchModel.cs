using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Web.Models.Catalog;

namespace Nop.Plugin.Widgets.BsProductLocation.Models
{
   public class CustomSearchModel
    {

       public CustomSearchModel()
       {
           SearchModel=new SearchModel();
           AvailableServiceAreas=new List<SelectListItem>();
       }
        public int ServiceAreaId { get; set; }

        public string ServiceAreaName { get; set; }
        public IList<SelectListItem> AvailableServiceAreas { get; set; }

        public SearchModel SearchModel { get; set; } 
        }
    
}
