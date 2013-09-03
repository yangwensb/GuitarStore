using Application.MainBoundedContext.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.MainBoundedContext
{
    public interface IInventoryAppService
    {
        IEnumerable<InventoryListDTO> GetInventoryList(int pageIndex, int pageCount, out int total, 
            InventoryListDTO.Filds orderByField = InventoryListDTO.Filds.Id, bool ascending = true, string keywords = null, bool getTotal = true);
    }
}
