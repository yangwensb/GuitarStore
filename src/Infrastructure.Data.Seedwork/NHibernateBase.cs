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

        public NHibernateBase(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        public ISession Session
        { 
            get
            {
                if (_session == null)
                {
                    _session = SessionFactory.OpenSession();
                }
                return _session;
            }
        }

        public IStatelessSession StatelessSession
        {
            get
            {
                if (_statelessSession == null)
                { 
                    _statelessSession = SessionFactory.OpenStatelessSession();
                }
                return _statelessSession;
            }
        }
       
        public IList<T> ExecuteICriteria<T>()
        {
            using (ITransaction transaction = Session.BeginTransaction())
            {
                try
                {
                    IList<T> result = Session.CreateCriteria(typeof(T)).List<T>();
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
