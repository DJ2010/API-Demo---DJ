using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace recruit_dotnetframework.Services.Logs
{
    public class LogService : ILogService
    {
        public void LogInfo(string message)
        {
            //stubbed code, can be replaced with log4net or write to file/db
            Debug.WriteLine($"[INFO] {DateTime.UtcNow} - {message}");
        }
        public void LogError(string message, Exception ex = null)
        {
            Debug.WriteLine($"[ERROR] {DateTime.UtcNow} - {message}");

            if (ex != null)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

    }
}