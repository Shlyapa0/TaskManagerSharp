using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProcessList
{
static partial class Program
{
    static void KillProcessByName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            if (processes.Length == 0)
            {
                Console.WriteLine($"No processes found with the name '{processName}' to kill.");
                return;
            }

            foreach (Process process in processes)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                    Console.WriteLine($"Process '{processName}' (ID: {process.Id}) killed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error killing process '{processName}' (ID: {process.Id}): {ex.Message}");
                }
            }
        }
}
}