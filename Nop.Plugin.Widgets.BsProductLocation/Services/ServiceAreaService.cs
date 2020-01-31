using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Plugin.Widgets.BsProductLocation.Domain;
using Nop.Services.Common;
using Nop.Services.Events;

namespace Nop.Plugin.Widgets.BsProductLocation.Services
{
    /// <summary>
    /// Service Area service
    /// </summary>
    public partial class ServiceAreaService : IServiceAreaService
    {
        #region Fields

        private readonly IRepository<ServiceAreaRecord> _serviceAreaRepository;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        public ServiceAreaService(IRepository<ServiceAreaRecord> serviceAreaRepository,
            IEventPublisher eventPublisher)
        {
            this._serviceAreaRepository = serviceAreaRepository;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a service Area record
        /// </summary>
        /// <param name="serviceArea">Service Area</param>
        public virtual void DeleteServiceArea(ServiceAreaRecord serviceArea)
        {
            if (serviceArea == null)
                throw new ArgumentNullException("serviceArea");

            _serviceAreaRepository.Delete(serviceArea);

            //event notification
            _eventPublisher.EntityDeleted(serviceArea);
        }

        /// <summary>
        /// Gets a service Area record by identifier
        /// </summary>
        /// <param name="serviceAreaId">Service Area identifier</param>
        /// <returns>Service Area</returns>
        public virtual ServiceAreaRecord GetServiceAreaById(int serviceAreaId)
        {
            if (serviceAreaId == 0)
                return null;

            return _serviceAreaRepository.GetById(serviceAreaId);
        }

    
        /// <summary>
        /// Gets a service Area statistics
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>A list service Area report lines</returns>
        public virtual IPagedList<ServiceAreaRecord> GetServiceAreas(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _serviceAreaRepository.Table;
            query = query.OrderBy(x => x.Id);
                        

            var result = new PagedList<ServiceAreaRecord>(query, pageIndex, pageSize);
            return result;
        }

        /// <summary>
        /// Inserts a service Area record
        /// </summary>
        /// <param name="serviceArea">Service Area</param>
        public virtual void InsertServiceArea(ServiceAreaRecord serviceArea)
        {
            if (serviceArea == null)
                throw new ArgumentNullException("serviceArea");

            _serviceAreaRepository.Insert(serviceArea);

            //event notification
            _eventPublisher.EntityInserted(serviceArea);
        }

        /// <summary>
        /// Updates the service Area record
        /// </summary>
        /// <param name="serviceArea">Service Area</param>
        public virtual void UpdateServiceArea(ServiceAreaRecord serviceArea)
        {
            if (serviceArea == null)
                throw new ArgumentNullException("serviceArea");

            _serviceAreaRepository.Update(serviceArea);

            //event notification
            _eventPublisher.EntityUpdated(serviceArea);
        }
        
        #endregion
    }
}