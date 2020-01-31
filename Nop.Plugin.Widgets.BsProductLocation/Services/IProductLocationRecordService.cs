using System.Collections.Generic;
using Nop.Plugin.Widgets.BsProductLocation.Domain;
using Nop.Core;

namespace Nop.Plugin.Widgets.BsProductLocation.Services
{
    public partial interface IProductLocationRecordService
    {
        void DeleteProductLocationRecord(ProductLocationRecord productLocationRecord);

        IList<ProductLocationRecord> GetAll();

        /// <summary>
        /// Gets ProductLocationRecord
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="productId"></param>
        /// <param name="serviceAreaId"></param>
        /// <returns>Product Locations</returns>
        IPagedList<ProductLocationRecord> GetProductLocationRecords(int pageIndex = 0, int pageSize = 2147483647, int productId = 0, int serviceAreaId = 0);
        ProductLocationRecord GetById(int googleProductRecordId);

        IList<ProductLocationRecord> GetByProductId(int productId);

        void InsertProductLocationRecord(ProductLocationRecord productLocationRecord);

        void UpdateProductLocationRecord(ProductLocationRecord productLocationRecord);
    }
}
