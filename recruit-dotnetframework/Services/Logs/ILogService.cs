using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recruit_dotnetframework.Services.Logs
{
    public interface ILogService
    {
        void LogInfo(string message);
        void LogError(string message, Exception ex = null);
    }
}
