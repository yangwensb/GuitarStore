using Infrastructure.Crosscutting.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Infrastructure.Crosscutting.Framework.Logging
{
    public class Log4netFactory : ILoggerFactory
    {
        private const string DEFAULT_NAME = "DefaultLogger";

        public string LoggerName { get; set; }
        public ILogger Create()
        {
            log4net.Config.XmlConfigurator.Configure();
            return new Log4netAdapter(LogManager.GetLogger(LoggerName ?? DEFAULT_NAME));   
        }
    }
}
