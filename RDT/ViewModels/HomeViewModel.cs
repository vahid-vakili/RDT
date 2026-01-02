using CredentialManagement;
using RDT.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace RDT.ViewModels
{
    public class HomeViewModel : ObservableObject
    {
        DataModel DM = new DataModel();
        public string CurrentLoginInfo { get; }
        public string ApplicationInfo { get; }
        public string UsersCount { get; }
        public string Time { get; }

        public HomeViewModel()
        {
            CurrentLoginInfo = "User: " + DataModel.UserName + Environment.NewLine + "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
            ApplicationInfo = $"Created by vahid (https://github.com/vahid-vakili)"
                                + Environment.NewLine + "Application Details:"
                                + Environment.NewLine + "ver. 1.0.0.0";
        }


        private ICommand _windowsRemote;
        public ICommand WindowsRemote
        {
            get
            {
                if (_windowsRemote == null)
                    _windowsRemote = new RelayCommand(WindowsRemoteMethod);
                return _windowsRemote;
            }
        }
        private void WindowsRemoteMethod(object obj)
        {
            DM.loadSelectedTergets();
            foreach (var item in DM.TargetsArray)
            {
                var cm = new Credential();
                if (!string.IsNullOrEmpty(item))
                {
                    try
                    {
                        cm.Target = item;
                        if (CredentialManager.IsValidCredential(cm))
                        {
                            Sessions.GetSession(item, SessionTypes.WinRemoteDesktop);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);

                    }
                }
            }
            if (DM.AutoClose) Application.Current.Shutdown();
        }


        private ICommand _ssh;
        public ICommand SSH
        {
            get
            {
                if (_ssh == null)
                    _ssh = new RelayCommand(SSHMethod);
                return _ssh;
            }
        }
        private void SSHMethod(object obj)
        {
            DM.loadSelectedTergets();
            foreach (var item in DM.TargetsArray)
            {
                var cm = new Credential();
                if (!string.IsNullOrEmpty(item))
                {
                    try
                    {
                        cm.Target = item;
                        if (CredentialManager.IsValidCredential(cm))
                        {
                            cm.Load();
                            string hostname = item, username = cm.Username, pass = cm.Password;
                            Sessions.GetSession(hostname, SessionTypes.SSHLinuxRemote, username, pass);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            if (DM.AutoClose) Application.Current.Shutdown();
        }

        private ICommand _ping;
        public ICommand Ping
        {
            get
            {
                if (_ping == null)
                    _ping = new RelayCommand(PingMethod);
                return _ping;
            }
        }
        private void PingMethod(object obj)
        {
            DM.loadSelectedTergets();
            foreach (var item in DM.TargetsArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    try
                    {
                        string command = "/C ping " + item;
                        Process.Start("cmd.exe", command);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private ICommand _web;
        public ICommand Web
        {
            get
            {
                if (_web == null)
                    _web = new RelayCommand(WebMethod);
                return _web;
            }
        }
        private void WebMethod(object obj)
        {
            Process.Start(DataModel.WebAddress);
        }


        private ICommand _config;
        public ICommand Config
        {
            get
            {
                if (_config == null)
                    _config = new RelayCommand(ConfigMethod);
                return _config;
            }
        }
        private void ConfigMethod(object obj)
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\RDTConfig.xml"))
                {
                    Process rdcProcess = new Process();
                    rdcProcess.StartInfo.FileName = Environment.ExpandEnvironmentVariables(AppDomain.CurrentDomain.BaseDirectory + @"\RDTConfig.xml");
                    rdcProcess.Start();
                }
                else MessageBox.Show("RDTConfig.xml not exist in Application path");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
    }
}
