using Domain.MainBoundedContext.Aggregates.InventoryAgg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.Data.MainBoundedContext.StoreModule.Repositories;
using Infrastructure.Data.Seedwork;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MainBoundedContext
{
    public class NinjectDependencyResolver : NinjectModule
    {
        ///<summary>
        /// Sets up NHibernate, and adds an ISessionFactory to the given
        /// container.
        ///</summary>
        private void ConfigureNHibernate()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(dbi =>
            {
                dbi.Dialect<MsSql2008Dialect>(); 
                dbi.Driver<SqlClientDriver>(); 
                dbi.ConnectionProvider<DriverConnectionProvider>();
                dbi.IsolationLevel = IsolationLevel.ReadCommitted; 
                dbi.Timeout = 15;
            });
            // Build the NHibernate ISessionFactory object
            var sessionFactory = FluentNHibernate
                                    .Cfg.Fluently.Configure(cfg)
                                    .Database(
                                        MsSqlConfiguration.MsSql2008.ConnectionString(
                                                c => c.FromConnectionStringWithKey("GuitarStore")))
                                                .CurrentSessionContext("call")
                                                .Mappings(m => m.FluentMappings
                                                    .AddFromAssemblyOf<Infrastructure.Data.MainBoundedContext.StoreModule.Repositories.StoreUnitOfWork>()
                                                    ).BuildSessionFactory();
           
            // Add the ISessionFactory instance to the container
            Bind<ISessionFactory>().ToConstant(sessionFactory);
            // Configure a resolver method to be used for creating ISession objects
            Bind<ISession>().ToMethod(CreateSession);
            // Configure a resolver method to be used for creating IStatelessSession objects
            Bind<IStatelessSession>().ToMethod(CreateStatelessSession);
        }

        ///<summary>
        /// Method used to create instances of ISession objects
        /// and bind them to the current context.
        ///</summary>
        private ISession CreateSession(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();

            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                // Open new ISession and bind it to the current session context
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return sessionFactory.GetCurrentSession();
        }

        ///<summary>
        /// Method used to create instances of IStatelessSession objects
        ///</summary>
        private IStatelessSession CreateStatelessSession(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();

            // Open new IStatelessSession and bind it to the current session context
            var session = sessionFactory.OpenStatelessSession();

            return session;
        }

        public override void Load()
        {
            ConfigureNHibernate();
            Bind<IQueryableUnitOfWork>().To<StoreUnitOfWork>();
            Bind<IInventoryRepository>().To<InventoryRepository>();
        }
    }
}

