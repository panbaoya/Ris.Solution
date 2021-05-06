using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Tools.Nlog
{
    public class NLogger
    {
        public static void LogError(string msg, Exception ex = null)
        {
            Logger logger = LogManager.GetLogger("Ris");
            logger.Error(ex,msg);
        }

        public static void LogInfo(string msg,string workID=null)
        {
            Logger logger = LogManager.GetLogger("Ris");
            var ei = new LogEventInfo(LogLevel.Info,logger.Name, msg);
            ei.Properties["WorkID"] = workID;
            ei.Message = msg;
            logger.Log(LogLevel.Info, ei);
        }
    }
}