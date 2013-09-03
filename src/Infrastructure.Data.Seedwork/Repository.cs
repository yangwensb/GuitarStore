using Domain.Seedwork;
using Infrastructure.Crosscutting.Logging;
using Infrastructure.Data.Seedwork.Resources;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace Infrastructure.Data.Seedwork
{
    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TEntity">The type of underlying entity in this repository</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        #region Members

        IQueryableUnitOfWork _unitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Add(TEntity item)
        {
            //if (item != (TEntity)null)
            //    GetSet().Add(item); // add new item in this set
            //else
            //{
            //    LoggerFactory.CreateLog()
            //              .LogInfo(Messages.info_CannotAddNullEntity, typeof(TEntity).ToString());

            //}
        }
        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Remove(TEntity item)
        {
            //if (item != (TEntity)null)
            //{
            //    //attach item if not exist
            //    _unitOfWork.Attach(item);

            //    //set as "removed"
            //    GetSet().Remove(item);
            //}
            //else
            //{
            //    LoggerFactory.CreateLog()
            //              .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            //}
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void TrackItem(TEntity item)
        {
            throw new NotImplementedException();

            //if (item != (TEntity)null)
            //    _UnitOfWork.Attach<TEntity>(item);
            //else
            //{
            //    LoggerFactory.CreateLog()
            //              .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            //}
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Modify(TEntity item)
        {
            if (item != (TEntity)null)
                _unitOfWork.SetModified(item);
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="id"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual TEntity Get(Guid id)
        {
            var session = _unitOfWork.GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                if (id != Guid.Empty)              
                    return session.Get<TEntity>(id);
                return null;
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            var session = _unitOfWork.GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                return session.Query<TEntity>().ToList();
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="specification"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> AllMatching(Domain.Seedwork.Specification.ISpecification<TEntity> specification)
        {
            var session = _unitOfWork.GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                return session.Query<TEntity>().Where(specification.SatisfiedBy()).ToList();
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            var session = _unitOfWork.GetSession();
            using (ITransaction transaction = session.BeginTransaction())
            {
                return session.Query<TEntity>().Where(filter).ToList();
            }
        }

        /// <summary>
        ///  <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></typeparam>
        /// <param name="pageIndex"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="total"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="specification"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="getTotal"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, out int total,
            System.Linq.Expressions.Expression<Func<TEntity, KProperty>> orderByExpression,
            bool ascending = true,
            Domain.Seedwork.Specification.ISpecification<TEntity> specification = null,
            bool getTotal = true)
        {
            IEnumerable<TEntity> query = null; 
            var session = _unitOfWork.GetSession();
            total = 0;

            using (ITransaction transaction = session.BeginTransaction())
            {               
                System.Linq.Expressions.Expression<Func<TEntity, bool>> filterByExpression =
                    specification == null ? p => true :
                    specification.SatisfiedBy();

                if (ascending)
                {
                    query = session.Query<TEntity>().Where(filterByExpression)
                                .OrderBy(orderByExpression)
                                .Skip(pageCount * pageIndex)
                                .Take(pageCount)
                                .ToFuture<TEntity>();
                }
                else
                {
                    query = session.Query<TEntity>().Where(filterByExpression)
                             .OrderByDescending(orderByExpression)
                             .Skip(pageCount * pageIndex)
                             .Take(pageCount)
                             .ToFuture<TEntity>();
                }              

                if (getTotal)
                {
                    var countResult = session.Query<TEntity>().Where(filterByExpression).ToFuture<TEntity>();
                    total = countResult.Count();
                }

                return query.ToList<TEntity>();
            }
        }

        /// <summary>
        /// <see cref="Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="persisted"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="current"><see cref="Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
