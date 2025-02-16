using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProcessList
{
static partial class Program
{
    static void KillProcessById(int processId)
        {
            try
            {
                Process process = Process.GetProcessById(processId);
                process.Kill();
                process.WaitForExit();
                Console.WriteLine($"Process with ID {processId} killed successfully.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Process with ID {processId} not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error killing process with ID {processId}: {ex.Message}");
            }
        }
}
}
