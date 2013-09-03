using Domain.Seedwork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.MainBoundedContext.Aggregates.InventoryAgg
{
    /// <summary>
    /// Base contract for inventory repository
    /// <see cref="Domain.Seedwork.IRepository{Inventory}"/>
    /// </summary>
    public interface IInventoryRepository
        : IRepository<Inventory>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="total"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="keywords"></param>
        /// <param name="getTotal"></param>
        /// <returns></returns>
        IList GetInventoryList(int pageIndex, int pageCount, out int total, 
                                string orderByExpression = null, bool ascending = true, string  keywords = null, bool getTotal = true);

    }
}
