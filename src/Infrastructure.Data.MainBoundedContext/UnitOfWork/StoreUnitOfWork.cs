using Domain.MainBoundedContext.Aggregates.InventoryAgg;
using Infrastructure.Data.Seedwork;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MainBoundedContext.StoreModule.Repositories
{
    public class StoreUnitOfWork : NHibernateBase, IQueryableUnitOfWork
    {

        #region IQueryableUnitOfWork
        public IList<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            var list = base.ExecuteICriteria<TEntity>();
            return list;
        }

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void CommitAndRefreshChanges()
        {
            throw new NotImplementedException();
        }

        public void RollbackChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
