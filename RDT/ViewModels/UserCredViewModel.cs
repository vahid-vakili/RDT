using CredentialManagement;
using RDT.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace RDT.ViewModels
{
    public class UserCredViewModel : ObservableObject
    {
        DataModel DM = new DataModel();
        private string _credentialsList;

        public string CredentialsList
        {
            get { return _credentialsList; }
            set
            {
                _credentialsList = value;
                OnPropertyChanged();
            }
        }
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged();
            }
        }

        public UserCredViewModel()
        {
            User = new User();
            User.Name = DataModel.UserName;

        }

        #region save
        private ICommand _save;
        public ICommand Save
        {
            get
            {
                if (_save == null)
                    _save = new RelayCommand(SaveMethod);
                return _save;
            }
        }
        private void SaveMethod(object obj)
        {
            if (!string.IsNullOrEmpty(User.Name) && !string.IsNullOrEmpty(User.Password))
            {
                if (!string.IsNullOrEmpty(User.Password))
                {
                    DM.loadSelectedTergets();
                    CredentialsList = String.Empty;
                    var cm = new Credential();
                    foreach (var item in DM.TargetsArray)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            cm.Target = item;
                            cm.Type = CredentialType.Generic;
                            cm.PersistanceType = PersistanceType.LocalComputer;
                            if (!cm.Exists())
                            {
                                cm.Username = User.Name.Trim();
                                cm.Password = User.Password.Trim();
                                cm.Save();
                                CredentialsList += Environment.NewLine + ($"Save Credential '{cm.Username}' to '{cm.Target}'");
                            }
                            else
                            {
                                cm.Load();
                                if (!string.IsNullOrEmpty(cm.Username))
                                {
                                    if (cm.Username.Equals(User.Name.Trim()))
                                    {
                                        cm.Username = User.Name.Trim();
                                        cm.Password = User.Password.Trim();
                                        cm.Save();
                                        CredentialsList += Environment.NewLine + ($"Change Credential '{cm.Username}' for '{cm.Target}'");
                                    }
                                    else
                                    {
                                        CredentialsList += Environment.NewLine + ($"Credentials for '{cm.Target}' has been exist by '{cm.Username}', " +
                                                                                  $" first Delete it and try again!");
                                    }

                                }
                            }
                        }

                    }
                }
            }
        }
        #endregion
        #region reload
        private ICommand reload;
        public ICommand Reload
        {
            get
            {
                if (reload == null)
                    reload = new RelayCommand(ReloadMethod);
                return reload;
            }
        }
        private void ReloadMethod(object obj)
        {
            DM.loadSelectedTergets();
            CredentialsList = string.Empty;
            var cm = new Credential();
            foreach (var item in DM.TargetsArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    cm.Target = item;
                    if (CredentialManager.IsValidCredential(cm))
                    {
                        cm.Load();
                        if (!string.IsNullOrEmpty(cm.Username))
                        {
                            CredentialsList += Environment.NewLine + ($"{cm.Username} -----------------> {cm.Target}");
                            OnPropertyChanged(nameof(CredentialsList));
                        }
                    }
                }
            }
        }

        #endregion
        #region delete
        private ICommand _delete;
        public ICommand Delete
        {
            get
            {
                if (_delete == null)
                    _delete = new RelayCommand(DeleteMethod);
                return _delete;
            }
        }
        private void DeleteMethod(object obj)
        {
            if ((MessageBox.Show("Are You Sure to Delete All Selected Credentials?", "Delete All", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                DM.loadSelectedTergets();
                CredentialsList = String.Empty;
                var cm = new Credential();
                foreach (var item in DM.TargetsArray)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        cm.Target = item;
                        if (cm.Exists())
                        {
                            CredentialsList += Environment.NewLine + ($"Deleted Credential for '{cm.Target}'");
                            cm.Delete();

                        }
                    }

                }
            }

        }
        #endregion

    }
}
