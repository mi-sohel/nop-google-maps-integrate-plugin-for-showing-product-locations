using Nop.Core;

namespace Nop.Plugin.Widgets.BsProductLocation.Domain
{
    /// <summary>
    /// Represents a  product location record
    /// </summary>
    public partial class ProductLocationRecord : BaseEntity
    {
        public int ProductId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int ServiceAreaId { get; set; }

    }
}