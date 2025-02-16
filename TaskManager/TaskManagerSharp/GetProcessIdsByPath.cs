using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProcessList
{
    static partial class Program
{
    static void KillProcessByFullPath(string fullPath)
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {
                    string processPath = "";
                    try
                    {
                        processPath = process.MainModule.FileName;
                    }
                    catch { processPath = "N/A"; }
                    if (string.Equals(processPath, fullPath, StringComparison.OrdinalIgnoreCase) && !string.Equals("N/A", fullPath, StringComparison.OrdinalIgnoreCase))
                    {
                        process.Kill();
                        process.WaitForExit();
                        Console.WriteLine($"Process '{process.ProcessName}' (ID: {process.Id}) with full path '{fullPath}' killed successfully.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error killing process '{process.ProcessName}' (ID: {process.Id}) with full path '{fullPath}': {ex.Message}");
                }
            }

            Console.WriteLine($"No process found with the full path '{fullPath}'.");
        }
}
}
