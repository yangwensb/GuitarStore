using Infrastructure.Crosscutting.Logging;
using Presentation.Seedwork.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Presentation.Filters
{
    public class DiagnosticFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Log Action Filter Call
            var message = new DiagnosticMessage
            {
                IP = filterContext.HttpContext.Request.UserHostAddress.ToString(),
                OrgId = 75,
                PersonId = 3,
                Session = "Session descipton"
            };

            LoggerFactory.CreateLog().Debug(message.MessageFormat, message.Parameters);

            this.OnActionExecuting(filterContext);
        }
    }
}
