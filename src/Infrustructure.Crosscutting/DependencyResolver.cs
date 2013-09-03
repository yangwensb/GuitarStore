
using log4net;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Crosscutting.Configuration
{
    public class DependencyResolver : NinjectModule
    {
        ///<summary>
        /// Set up log4net for this application, including putting it in the given container.
        ///</summary>
        private void ConfigureLog4net(IKernel container)
        {
            log4net.Config.XmlConfigurator.Configure();
            var logger = LogManager.GetLogger("Infrastructure.Data");
            container.Bind<ILog>().ToConstant(logger);
        }

        public override void Load()
        {
            ConfigureLog4net(this.Kernel);
        }
    }
}
