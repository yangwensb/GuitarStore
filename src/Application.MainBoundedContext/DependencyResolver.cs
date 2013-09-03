using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Crosscutting.Adapter;
using Infrastructure.Crosscutting.Framework.Adapter;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace Application.MainBoundedContext
{
    public class DependencyResolver : NinjectModule
    {     
        public override void Load()
        {
            Bind<IInventoryAppService>().To<InventoryAppService>();
            Bind<ITypeAdapterFactory>().To<AutomapperTypeAdapterFactory>();
        }
    }
}

