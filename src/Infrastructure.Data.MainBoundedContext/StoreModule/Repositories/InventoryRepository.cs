using System.Data;
using System.Diagnostics.Contracts;
using Domain.MainBoundedContext.Aggregates.InventoryAgg;
using Infrastructure.Data.Seedwork;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MainBoundedContext.StoreModule.Repositories
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        #region Constructor

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="unitOfWork">Associated unit of work</param>
        public InventoryRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #endregion

        #region Overrides

        /// <summary>
        /// Get all inventory in the store
        /// </summary>
        /// <returns>Enumerable collection of inventory</returns>
        public override IEnumerable<Inventory> GetAll()
        {
            var currentUnitOfWork = UnitOfWork as IQueryableUnitOfWork;

            var session = currentUnitOfWork.GetSession();

            return session.CreateCriteria(typeof(Inventory))
                .SetFetchMode("Type", NHibernate.FetchMode.Join).List<Inventory>();
        }

        /// <summary>
        /// Get inventory in the store
        /// </summary>
        /// <returns>Inventory with all entities attached to the object</returns>
        public override Inventory Get(Guid id)
        {
            // Lazy loading by default
            var inventory = base.Get(id);
            // Force get value objects for Type property
            NHibernateUtil.Initialize(inventory.Type);

            return inventory;
        }
        #endregion

        #region Public Methods

        public System.Collections.IList GetInventoryList(int pageIndex, int pageCount, out int total, string fieldName = null,
              bool ascending = true, string keywords = null, bool getTotal = true)
        {
            var queryableUnitOfWork = UnitOfWork as IQueryableUnitOfWork;

            Contract.Assert(queryableUnitOfWork != null, "UoW is not initialized for InventoryRepository");

            var session = queryableUnitOfWork.GetStatelessSession();
            total = 0;

            var extention = keywords == null ? " OR 1=1" : "";
            extention += " ORDER BY " + (fieldName == null ? "Id ASC" :
                String.Format("{0} {1}", fieldName, @ascending ? "ASC" : "DESC"));

            keywords = keywords == null ? null : String.Format(" \"{0}*\" ",keywords);

            using (var transaction = session.BeginTransaction())
            {
                var query = session.CreateSQLQuery(
                    session.GetNamedQuery("InventoryList")
                        .QueryString + extention
                    )
                    .SetParameter("keywords", keywords ?? "*")
                    .SetFirstResult(pageIndex * pageCount)
                    .SetMaxResults(pageCount);

                if (!getTotal) return query.List();

                var count = session.CreateSQLQuery(
                    session.GetNamedQuery("InventoryListCount")
                        .QueryString + (keywords == null ? " OR 1=1" : "")
                    )
                    .SetParameter("keywords", keywords ?? "*");
                total = count.UniqueResult<int>();

                return query.List();
            }
        }

        #endregion
    }
}
