using Infrastructure.Crosscutting.Logging;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrustructure.Crosscutting.Logging
{
    [Serializable]
    public class DiagnosticAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            LoggerFactory.CreateLog().Debug(String.Format("Method [{0}] entry.", args.Method.Name));
        }
    }
}
