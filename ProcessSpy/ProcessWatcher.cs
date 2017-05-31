using System.Diagnostics;
using System.IO;

namespace ProcessSpy
{
    public class ProcessWatcher
    {
        private static Process process;

        /// <summary>
        /// Load a native application
        /// </summary>
        /// <param name="exePath"></param>
        /// <param name="parameters"></param>
        public static void LoadApplication(string exePath, string parameters = "", bool debug = false)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = exePath;
            //startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = parameters;

            if (debug)
            {
                using (StreamWriter writer = new StreamWriter(Path.GetTempPath() + "ProcessSpyDebug.txt"))
                {
                    writer.WriteLine("FileName: " + startInfo.FileName);
                    writer.WriteLine("Arguments: " + startInfo.Arguments);
                    writer.Close();
                    writer.Dispose();
                }
            }

            process = Process.Start(startInfo);
        }

        /// <summary>
        /// Test for if loaded application has closed
        /// </summary>
        /// <returns></returns>
        public static bool LoadedApplicationIsClosed()
        {
            return process.HasExited;
        }


        /// <summary>
        /// Check if a specific process is open by name
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool IsProcessOpen(string processName)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(processName)) return true;
            }
            return false;
        }

        /// <summary>
        /// Kills every instance of a named process
        /// </summary>
        /// <param name="processName"></param>
        public static void KillProcess (string processName)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(processName)) clsProcess.Kill();
            }
        }

    }
}