using Infrastructure.Crosscutting.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.Framework.Logging
{
    public class Log4netAdapter : ILogger
    {
        ILog _loggerForWebSite;
       
        public Log4netAdapter(ILog logger)
        {
            _loggerForWebSite = logger;
        }

        public void Debug(string message, params object[] args)
        {
            _loggerForWebSite.DebugFormat(message, args);
        }

        public void Debug(string message, Exception exception, params object[] args)
        {
            StringBuilder sb = new StringBuilder( message);
            sb.AppendFormat("Exception details: {0}", exception.ToString());
            _loggerForWebSite.DebugFormat(message, args);
        }

        public void Debug(object item)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void LogError(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void LogError(string message, Exception exception, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
