using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Nop.Core.Data;
using Nop.Plugin.Widgets.BsProductLocation.Domain;
using Nop.Core;

namespace Nop.Plugin.Widgets.BsProductLocation.Services
{
    public partial class ProductLocationRecordService : IProductLocationRecordService
    {
        #region Fields

        private readonly IRepository<ProductLocationRecord> _productLocationRecordRepository;

        #endregion

        #region Ctor

        public ProductLocationRecordService(IRepository<ProductLocationRecord> productLocationRecordRepository)
        {
            this._productLocationRecordRepository = productLocationRecordRepository;
        }

        #endregion

    

        #region Methods

        public virtual void DeleteProductLocationRecord(ProductLocationRecord productLocationRecord)
        {
            if (productLocationRecord == null)
                throw new ArgumentNullException("productLocationRecord");

            _productLocationRecordRepository.Delete(productLocationRecord);
        }

        public virtual IList<ProductLocationRecord> GetAll()
        {
            var query = from gp in _productLocationRecordRepository.Table
                        orderby gp.Id
                        select gp;
        
            var records = query.ToList();
            return records;
        }
        public virtual IPagedList<ProductLocationRecord> GetProductLocationRecords(
           int pageIndex = 0, int pageSize = int.MaxValue, int productId = 0,int serviceAreaId=0)
        {
            var query = _productLocationRecordRepository.Table;
            query = query.OrderBy(x => x.Id);

            if (productId > 0)
            {
                query = query.Where(x => x.ProductId.Equals(productId));
            }
            if (serviceAreaId>0)
            {
                query = query.Where(x => x.ServiceAreaId.Equals(serviceAreaId));
            }

            query = query.OrderBy(x => x.Id);
        
            var products = new PagedList<ProductLocationRecord>(query, pageIndex, pageSize);
            return products;
        }
        public virtual ProductLocationRecord GetById(int productLocationRecordId)
        {
            if (productLocationRecordId == 0)
                return null;

            return _productLocationRecordRepository.GetById(productLocationRecordId);
        }

        public virtual IList<ProductLocationRecord> GetByProductId(int productId)
        {
            if (productId == 0)
                return new List<ProductLocationRecord>();

            var query = from gp in _productLocationRecordRepository.Table
                        where gp.ProductId == productId
                        orderby gp.Id
                        select gp;
     
            return query.ToList();
        }

        public virtual void InsertProductLocationRecord(ProductLocationRecord productLocationRecord)
        {
            if (productLocationRecord == null)
                throw new ArgumentNullException("productLocationRecord");

            _productLocationRecordRepository.Insert(productLocationRecord);
        }

        public virtual void UpdateProductLocationRecord(ProductLocationRecord productLocationRecord)
        {
            if (productLocationRecord == null)
                throw new ArgumentNullException("productLocationRecord");

            _productLocationRecordRepository.Update(productLocationRecord);
        }

        #endregion
    }
}
