using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Plugin.Widgets.BsProductLocation.Domain;

namespace Nop.Plugin.Widgets.BsProductLocation.Services
{
    /// <summary>
    /// Service Area service interafce
    /// </summary>
    public partial interface IServiceAreaService
    {
        /// <summary>
        /// Deletes a service Area record
        /// </summary>
        /// <param name="serviceArea">Service Area</param>
        void DeleteServiceArea(ServiceAreaRecord serviceArea);

        /// <summary>
        /// Gets a service Area record by identifier
        /// </summary>
        /// <param name="serviceAreaId">Service Area identifier</param>
        /// <returns>Service Area</returns>
        ServiceAreaRecord GetServiceAreaById(int serviceAreaId);

       
      
        /// <summary>
        /// Inserts a service Area record
        /// </summary>
        /// <param name="serviceArea">Service Area</param>
        void InsertServiceArea(ServiceAreaRecord serviceArea);

        /// <summary>
        /// Updates the service Area record
        /// </summary>
        /// <param name="serviceArea">Service Area</param>
        void UpdateServiceArea(ServiceAreaRecord serviceArea);

        IPagedList<ServiceAreaRecord> GetServiceAreas(int pageIndex = 0, int pageSize = int.MaxValue);

    }
}