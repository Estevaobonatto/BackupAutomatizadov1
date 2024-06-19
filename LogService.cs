using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupAutomatizado8
{
    public static class LogService
    {
        private static readonly List<string> logs = new List<string>();

        public static void Log(string message)
        {
            logs.Add(message);
        }

        public static IReadOnlyList<string> GetLogs()
        {
            return logs.AsReadOnly();
        }

        public static void ClearLogs()
        {
            logs.Clear();
        }
    }
}
