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
    public class MainBCUnitOfWork : NHibernateBase, IQueryableUnitOfWork
    {
        public MainBCUnitOfWork(ISessionFactory sessionFactory)
            : base(sessionFactory)
        {
        }

        #region IQueryableUnitOfWork
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

        public ISession GetSession()
        {
            return Session;
        }

        public IStatelessSession GetStatelessSession()
        {
            return StatelessSession;
        }
    }
}
