using Infrastructure.Crosscutting.Framework.Logging;
using Infrastructure.Crosscutting.Logging;
using Ninject;
using Ninject.Modules;

namespace Infrastructure.Crosscutting.Framework
{
    public class DependencyResolver : NinjectModule
    {
        ///<summary>
        /// Set up log4net for this application, including putting it in the given container.
        ///</summary>
        private static void ConfigureLog4Net(IKernel container)
        {
            var factory = new Log4netFactory();
            LoggerFactory.SetCurrent(factory);
        }

        public override void Load()
        {
            ConfigureLog4Net(this.Kernel);
        }
    }
}
