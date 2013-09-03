using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seedwork
{
    public abstract class NHibernateBase
    {
        private static Configuration Configuration { get; set; }
        protected static ISessionFactory SessionFactory { get; set; }
        private static ISession _session = null;
        private static IStatelessSession _statelessSession = null;

        public static Configuration ConfigureNHibernate(Assembly assembly)
        {
            Configuration = new Configuration();

            Configuration.DataBaseIntegration(dbi =>
            {
                dbi.Dialect<MsSql2008DialectFTS>();
                dbi.Driver<SqlClientDriver>();
                dbi.ConnectionProvider<DriverConnectionProvider>();
                dbi.IsolationLevel = IsolationLevel.ReadCommitted;
                dbi.Timeout = 15;
                dbi.ConnectionStringName = "GuitarStore";    
                
            });

            Configuration.AddAssembly(assembly);
            return Configuration;
        }

        public void Initialize(Assembly assembly)
        {
            Configuration = ConfigureNHibernate(assembly);
            SessionFactory = FluentNHibernate
                .Cfg.Fluently.Configure(Configuration)
                .Mappings(m => m.FluentMappings.AddFromAssembly(assembly))
                .BuildSessionFactory();
        }

        public static ISession Session
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
        
        public static IStatelessSession StatelessSession
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
