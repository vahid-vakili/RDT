using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace RDT.Models
{
    public enum SessionTypes
    {
        WinRemoteDesktop = 1,
        SSHLinuxRemote = 2
    }
    public class Sessions
    {
        public static void GetSession(string hostname, SessionTypes SessionType, string sSHUsername = null, string sSHpass = null)
        {
            Process rdcProcess = new Process();
            if (SessionType.Equals(SessionTypes.WinRemoteDesktop))
            {
                rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\system32\mstsc.exe");
                rdcProcess.StartInfo.Arguments = "/v " + hostname;
                rdcProcess.Start();
            }
            else if (SessionType.Equals(SessionTypes.SSHLinuxRemote))
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\putty.exe"))
                {
                    rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(AppDomain.CurrentDomain.BaseDirectory + @"\putty.exe");
                    rdcProcess.StartInfo.Arguments = $"-ssh {sSHUsername}@{hostname} -pw {sSHpass}";
                    rdcProcess.Start();
                }
                else MessageBox.Show("putty.exe not exist in root directory");
            }

        }
    }
}
