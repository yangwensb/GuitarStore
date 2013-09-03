using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrastructure.Crosscutting.Adapter
{
    /// <summary>
    /// Base contract for adapter factory
    /// </summary>
    public interface  ITypeAdapterFactory
    {
        /// <summary>
        /// Create a type adater
        /// </summary>
        /// <returns>The created ITypeAdapter</returns>
        ITypeAdapter Create();

        void Initialize(params Assembly [] assembly);
    }
}
