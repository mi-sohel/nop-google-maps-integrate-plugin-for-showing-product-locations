using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.BsProductLocation.Models
{
    public class PublicInfoModel : BaseNopModel
    {
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string ProductSeName { get; set; }
     
    }
}