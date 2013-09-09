
using Infrastructure.Crosscutting.Framework.Logging;
using Infrastructure.Crosscutting.Logging;
using log4net;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Crosscutting.Framework
{
    public class DependencyResolver : NinjectModule
    {
        ///<summary>
        /// Set up log4net for this application, including putting it in the given container.
        ///</summary>
        private void ConfigureLog4net(IKernel container)
        {
            var factory = new Log4netFactory();
            LoggerFactory.SetCurrent(factory);
        }

        public override void Load()
        {
            ConfigureLog4net(this.Kernel);
        }
    }
}
