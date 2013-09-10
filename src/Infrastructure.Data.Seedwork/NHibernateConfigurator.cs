using Infrustructure.Crosscutting.Logging;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seedwork
{
    public static class NHibernateConfigurator
    { 
        [Diagnostic]
        public static ISessionFactory Configure(Assembly assembly)
        {
            var configuration = new Configuration();

            configuration.DataBaseIntegration(dbi =>
            {
                dbi.Dialect<MsSql2008DialectFTS>();
                dbi.Driver<SqlClientDriver>();
                dbi.ConnectionProvider<DriverConnectionProvider>();
                dbi.IsolationLevel = IsolationLevel.ReadCommitted;
                //TODO: Read from configuration file
                dbi.Timeout = 15; 
                dbi.ConnectionStringName = "GuitarStore";

            });

            configuration.AddAssembly(assembly);

            return FluentNHibernate
                        .Cfg.Fluently.Configure(configuration)
                        .Mappings(m => m.FluentMappings.AddFromAssembly(assembly))
                        .BuildSessionFactory(); 
        }

    }
}
