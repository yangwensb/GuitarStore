﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Crosscutting.Configuration
{
   public class NinjectConfigurator
{
///<summary>
/// Entry method used by caller to configure the given
/// container with all of this application's
/// dependencies. Also configures the container as this
/// application's dependency resolver.
///</summary>
public void Configure(IKernel container)
{
// Add all bindings/dependencies
AddBindings(container);
// Use the container and the NinjectDependencyResolver as
// application's resolver
var resolver = new NinjectDependencyResolver(container);
GlobalConfiguration.Configuration.DependencyResolver = resolver;
}
///<summary>
/// Add all bindings/dependencies to the container
///</summary>
private void AddBindings(IKernel container)
    ConfigureNHibernate(container);
ConfigureLog4net(container);
container.Bind<IDateTime>().To<DateTimeAdapter>();
container.Bind<IDatabaseValueParser>().To<DatabaseValueParser>();
container.Bind<IHttpCategoryFetcher>().To<HttpCategoryFetcher>();
container.Bind<IHttpPriorityFetcher>().To<HttpPriorityFetcher>();
container.Bind<IHttpStatusFetcher>().To<HttpStatusFetcher>();
container.Bind<IHttpUserFetcher>().To<HttpUserFetcher>();
container.Bind<IHttpTaskFetcher>().To<HttpTaskFetcher>();
container.Bind<IUserManager>().To<UserManager>();
container.Bind<IMembershipAdapter>().To<MembershipAdapter>();
container.Bind<ICategoryMapper>().To<CategoryMapper>();
container.Bind<IPriorityMapper>().To<PriorityMapper>();
container.Bind<IStatusMapper>().To<StatusMapper>();
container.Bind<IUserMapper>().To<UserMapper>();
container.Bind<ISqlCommandFactory>().To<SqlCommandFactory>();
container.Bind<IUserRepository>().To<UserRepository>();
       container.Bind<IUserSession>().ToMethod(CreateUserSession).
InRequestScope();
}
///<summary>
/// Set up log4net for this application, including putting it in the
/// given container.
///</summary>
private void ConfigureLog4net(IKernel container)
{
log4net.Config.XmlConfigurator.Configure();
var loggerForWebSite = LogManager.GetLogger("Mvc4ServicesBookWebsite");
container.Bind<ILog>().ToConstant(loggerForWebSite);
}
///<summary>
/// Used to fetch the current thread's principal as
/// an <see cref="IUserSession"/> object.
///</summary>
private IUserSession CreateUserSession(IContext arg)
{
return new UserSession(Thread.CurrentPrincipal as GenericPrincipal);
}
///<summary>
/// Sets up NHibernate, and adds an ISessionFactory to the given
/// container.
///</summary>
private void ConfigureNHibernate(IKernel container)
{
// Build the NHibernate ISessionFactory object
var sessionFactory = FluentNHibernate
.Cfg.Fluently.Configure()
.Database(
MsSqlConfiguration.MsSql2008.ConnectionString(
c => c.FromConnectionStringWithKey("Mvc4ServicesDb")))
.CurrentSessionContext("web")
.Mappings(m =>
m.FluentMappings.AddFromAssemblyOf<SqlCommandFactory>())
.BuildSessionFactory();
// Add the ISessionFactory instance to the container
container.Bind<ISessionFactory>().ToConstant(sessionFactory);
// Configure a resolver method to be used for creating ISession objects
container.Bind<ISession>().ToMethod(CreateSession);
///<summary>
/// Method used to create instances of ISession objects
/// and bind them to the HTTP context.
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
}
}
