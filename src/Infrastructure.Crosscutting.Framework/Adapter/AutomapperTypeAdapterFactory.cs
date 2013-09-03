using System;
using System.Linq;
using AutoMapper;
using Infrastructure.Crosscutting.Adapter;
		
namespace Infrastructure.Crosscutting.Framework.Adapter
{

    public class AutomapperTypeAdapterFactory
        :ITypeAdapterFactory
    {
        #region Constructor

        /// <summary>
        /// Create a new Automapper type adapter factory
        /// </summary>
        public AutomapperTypeAdapterFactory()
        {
            //scan all assemblies finding Automapper Profile
            Initialize(AppDomain.CurrentDomain.GetAssemblies());
        }

        #endregion

        #region ITypeAdapterFactory Members

        public ITypeAdapter Create()
        {
            return new AutomapperTypeAdapter();
        }

        public void Initialize(params System.Reflection.Assembly[] profiles)
        {
            Mapper.Initialize(cfg =>
            {
                foreach (var item in profiles
                                    .SelectMany(a => a.GetTypes())
                                    .Where(t => t.BaseType == typeof(Profile)))
                {
                    if (item.FullName != "AutoMapper.SelfProfiler`2")
                        cfg.AddProfile(Activator.CreateInstance(item) as Profile);
                }
            });
        }

        #endregion
    }
}
