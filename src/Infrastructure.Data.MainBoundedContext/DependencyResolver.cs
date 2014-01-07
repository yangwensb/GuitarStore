using Domain.MainBoundedContext.Aggregates.InventoryAgg;
using Infrastructure.Data.MainBoundedContext.StoreModule.Repositories;
using Infrastructure.Data.Seedwork;
using NHibernate;
using Ninject.Modules;
using Ninject.Syntax;

namespace Infrastructure.Data.MainBoundedContext
{
    public class DependencyResolver : NinjectModule
    {     
        public override void Load()
        {
            Bind<IQueryableUnitOfWork>().To<MainBCUnitOfWork>();
            Bind<IInventoryRepository>().To<InventoryRepository>();

            ConfigureNHibernate(Kernel);
        }

        private static void ConfigureNHibernate(IBindingRoot container)
        {
            var sessionFactory = NHibernateConfigurator.Configure(typeof(MainBCUnitOfWork).Assembly);
            container.Bind<ISessionFactory>().ToConstant(sessionFactory);
        }
    }
}

