using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Application.MainBoundedContext.DTO;
using Domain.MainBoundedContext.Aggregates.InventoryAgg;
using Infrastructure.Crosscutting.Adapter;
using Application.Seedwork;
using Application.MainBoundedContext.DTO.Profiles;
using System.Linq.Expressions;

namespace Application.MainBoundedContext
{
    public class InventoryAppService : Application.MainBoundedContext.IInventoryAppService
    {
        #region Members
        private readonly IInventoryRepository _inventoryRepository;
        #endregion

        #region Constructor
        public InventoryAppService(IInventoryRepository inventoryRepository, ITypeAdapterFactory typeAdapterFactory)
        {
            Contract.Requires<ArgumentNullException>(inventoryRepository != null, "inventoryRepository");
            Contract.Requires<ArgumentNullException>(typeAdapterFactory != null, "typeAdapterFactory");

            typeAdapterFactory.Initialize(typeof(InventoryProfile).Assembly);
            TypeAdapterFactory.SetCurrent(typeAdapterFactory);
            _inventoryRepository = inventoryRepository;
        }
        #endregion

        public IEnumerable<InventoryListDTO> GetInventoryList(
                int pageIndex, int pageCount, out int total, 
                InventoryListDTO.Filds orderByField = InventoryListDTO.Filds.Id,
                bool ascending = true, string keywords = null, bool getTotal = true)
        {
          
            var list = _inventoryRepository.GetInventoryList(pageIndex, pageCount, out total, orderByField.ToString(), ascending, keywords, getTotal);

            return list.ProjectedAsCollection<InventoryListDTO>();
        }
    }
}
