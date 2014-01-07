using Application.MainBoundedContext.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.MainBoundedContext
{
    public interface IInventoryAppService
    {
        IEnumerable<InventoryListDto> GetInventoryList(int pageIndex, int pageCount, out int total, 
            InventoryListDto.Fields orderByField = InventoryListDto.Fields.Id, bool ascending = true, string keywords = null, bool getTotal = true);
    }
}
