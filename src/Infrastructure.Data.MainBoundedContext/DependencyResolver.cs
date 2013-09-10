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
    public class DependencyResolver : NinjectModule
    {     
        public override void Load()
        {
            Bind<IQueryableUnitOfWork>().To<MainBCUnitOfWork>();
            Bind<IInventoryRepository>().To<InventoryRepository>();

            ConfigureNHibernate(this.Kernel);
        }

        private void ConfigureNHibernate(IKernel container)
        {
            var sessionFactory = NHibernateConfigurator.Configure(typeof(MainBCUnitOfWork).Assembly);
            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
        }
    }
}

