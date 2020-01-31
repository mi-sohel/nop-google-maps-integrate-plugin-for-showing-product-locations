using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.BsProductLocation.Domain;

namespace Nop.Plugin.Widgets.BsProductLocation.Data
{
   public class ServiceAreaMap : NopEntityTypeConfiguration<ServiceAreaRecord>
    {
       public ServiceAreaMap()
        {
            this.ToTable("Pl_ServiceArea");
            this.HasKey(x => x.Id);
        }
    }
}
