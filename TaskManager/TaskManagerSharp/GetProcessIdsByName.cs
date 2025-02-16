using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProcessList
{
    static partial class Program
    {
        static List<int> GetProcessIdsByName(string processName)
        {
            List<int> processIds = new List<int>();
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                processIds.Add(process.Id);
            }
            return processIds;
        }

        static List<int> GetProcessIdsByPath(string fullPath)
        {
            List<int> processIds = new List<int>();
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                string processPath = "";
                try
                {
                    processPath = process.MainModule.FileName;
                }
                catch { processPath = "N/A"; }
                if (string.Equals(processPath, fullPath, StringComparison.OrdinalIgnoreCase) && !string.Equals("N/A", processPath, StringComparison.OrdinalIgnoreCase))
                {
                    processIds.Add(process.Id);
                }
            }
            return processIds;
        }
    }

    
}
