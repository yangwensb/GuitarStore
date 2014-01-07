using NHibernate;
using System;
using System.Collections.Generic;

namespace Infrastructure.Data.Seedwork
{
    public abstract class NHibernateBase
    {
        protected ISessionFactory SessionFactory { get; set; }
        protected ISession _session = null;
        protected IStatelessSession _statelessSession = null;

        protected NHibernateBase(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        public ISession Session
        { 
            get { return _session ?? (_session = SessionFactory.OpenSession()); }
        }

        public IStatelessSession StatelessSession
        {
            get { return _statelessSession ?? (_statelessSession = SessionFactory.OpenStatelessSession()); }
        }
       
        public IList<T> ExecuteICriteria<T>()
        {
            using (var transaction = Session.BeginTransaction())
            {
                try
                {
                    var result = Session.CreateCriteria(typeof(T)).List<T>();
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
