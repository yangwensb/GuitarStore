using Domain.Seedwork;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seedwork
{
    public interface IQueryableUnitOfWork
           : IUnitOfWork, ISql
    {
        /// <summary>
        /// Returns a ISession instance for access to entities of the given type  
        /// </summary>
        /// <returns></returns>
        ISession GetSession();

        /// <summary>
        /// Returns a ISession instance for access to entities of the given type  
        /// </summary>
        /// <returns></returns>
        IStatelessSession GetStatelessSession();

        /// <summary>
        /// Attach this item into "ObjectStateManager"
        /// </summary>
        /// <typeparam name="TValueObject">The type of entity</typeparam>
        /// <param name="item">The item <</param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Set object as modified
        /// </summary>
        /// <typeparam name="TValueObject">The type of entity</typeparam>
        /// <param name="item">The entity item to set as modifed</param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Apply current values in <paramref name="original"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="original">The original entity</param>
        /// <param name="current">The current entity</param>
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
    }
}
