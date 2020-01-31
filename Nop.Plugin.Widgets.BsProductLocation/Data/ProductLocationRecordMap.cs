using Nop.Data.Mapping;
using Nop.Plugin.Widgets.BsProductLocation.Domain;

namespace Nop.Plugin.Widgets.BsProductLocation.Data
{
    public partial class ProductLocationRecordMap : NopEntityTypeConfiguration<ProductLocationRecord>
    {
        public ProductLocationRecordMap()
        {
            this.ToTable("Pl_ProductLocation");
            this.HasKey(x => x.Id);
           

        }
    }
}