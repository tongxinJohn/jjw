using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zfkr.SubscribeMysql.Common.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Zfkr.SubscribeMysql.General
{
    public class LogOperation
    {
        private static ILogger _logger;
        public static ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    LogManager.Configuration = new XmlLoggingConfiguration("NLog.Config");
                    //设置 NLog
                    //CanalSharpLogManager.LoggerFactory.AddNLog();
                    //_logger = CanalSharpLogManager.LoggerFactory.CreateLogger("SubscribeMysql");

              
                 
                }
                return _logger;
            }
        }
    }
}
