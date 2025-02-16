using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProcessList
{
    static partial class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a command (searchId <processname>, kill <processname>, killid <processid>, killpath <fullpath>, list, exit):");

            while (true)
            {
                Console.Write("> ");
                string command = Console.ReadLine();

                if (string.IsNullOrEmpty(command)) //ignore empty input
                {
                    continue;
                }

                string[] parts = command.Split(' ');

                try
                {
                    switch (parts[0].ToLower())
                    {
                        case "searchid":
                            if (parts.Length > 1)
                            {
                                string processName = string.Join(" ", parts.Skip(1)); // Handles process names with spaces
                                List<int> specificProcessIds = GetProcessIdsByName(processName);

                                if (specificProcessIds.Count == 0)
                                {
                                    Console.WriteLine($"No processes found with the name '{processName}'.");
                                }
                                else
                                {
                                    Console.WriteLine($"Process IDs for '{processName}':");
                                    foreach (int processId in specificProcessIds)
                                    {
                                        Console.WriteLine(processId);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Usage: searchId <processname>");
                            }
                            break;

                        case "list":
                            int currentProcessId = Process.GetCurrentProcess().Id;
                            Process[] processList = Process.GetProcesses();

                            Console.WriteLine("-----------------------------------------------------------------------------------");
                            Console.WriteLine("| * | Process Name    | ID       | Full Path                                       |");
                            Console.WriteLine("-----------------------------------------------------------------------------------");

                            foreach (Process process in processList)
                            {
                                try
                                {
                                    string fullPath = "";
                                    try
                                    {
                                        fullPath = process.MainModule.FileName;
                                    }
                                    catch { fullPath = "N/A"; }

                                    string shortName = Path.GetFileNameWithoutExtension(fullPath);
                                    string marker = (process.Id == currentProcessId) ? "*" : " ";

                                    Console.WriteLine($"|{marker}| {process.ProcessName,-15} | {process.Id,-7} | {fullPath,-50} |");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error getting information for process ID {process.Id}: {ex.Message}");
                                }
                            }

                            Console.WriteLine("-----------------------------------------------------------------------------------");
                            Console.WriteLine($"Total processes running: {processList.Length}");
                            break;

                        case "kill":
                            if (parts.Length > 1)
                            {
                                string processName = string.Join(" ", parts.Skip(1));
                                KillProcessByName(processName);
                            }
                            else
                            {
                                Console.WriteLine("Usage: kill <processname>");
                            }
                            break;
                        
                        case "killid":
                            if (parts.Length > 1)
                            {
                                if (int.TryParse(parts[1], out int processIdToKill))
                                {
                                    KillProcessById(processIdToKill);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid process ID.  Must be an integer.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Usage: killid <processid>");
                            }
                            break;

                        case "killpath":
                            if (parts.Length > 1)
                            {
                                string fullPath = string.Join(" ", parts.Skip(1));
                                KillProcessByFullPath(fullPath);
                            }
                            else
                            {
                                Console.WriteLine("Usage: killpath <fullpath>");
                            }
                            break;

                        case "exit":
                            Console.WriteLine("Exiting...");
                            return; // Exit the program

                        default:
                            Console.WriteLine("Invalid command.  Available commands: searchId <processname>, list, exit");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }
        }
    }
}