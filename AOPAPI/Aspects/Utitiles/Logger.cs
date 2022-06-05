using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AOPAPI.Aspects.Utitiles
{
    public class Logger : ILogger
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Logger()
        {
            XmlConfigurator.Configure();
        }

        public void LogError(Exception exception)
        {
            _log.Error(exception.Message, exception);
        }

        public void LogDebug(string message)
        {
            _log.Debug(message);
        }
    }
}