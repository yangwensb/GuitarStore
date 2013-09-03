using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Crosscutting.Configuration
{
    public class NinjectDependencyResolver : NinjectModule
    {
        private readonly IKernel _container;
        public IKernel Container
        {
            get { return _container; }
        }
        public NinjectDependencyResolver(IKernel container)
        {
            _container = container;
        }
        public object GetService(Type serviceType)
        {
            return _container.TryGet(serviceType);
        }

        public void Dispose()
        {
            // noop
        }

        public override void Load()
        {
            throw new NotImplementedException();
        }
    }
}
