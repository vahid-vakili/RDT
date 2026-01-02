using RDT.Models;
using System;

namespace RDT.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand UserCredViewCommand { get; set; }
        DataModel DM = new DataModel();



        public HomeViewModel HVM { get; set; }
        public UserCredViewModel UsVM { get; set; }



        private string _lastError;

        public string LastError
        {
            get { return _lastError; }
            set
            {
                _lastError = value;
                OnPropertyChanged();
            }
        }
        private bool _successedLogin;
        public bool SuccessedLogin
        {
            get { return _successedLogin; }
            set
            {
                _successedLogin = value;
                OnPropertyChanged();
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            try
            {
                DM.FetchData();
                HVM = new HomeViewModel();
                UsVM = new UserCredViewModel();

                CurrentView = HVM;


                HomeViewCommand = new RelayCommand(o =>
                {
                    CurrentView = HVM;
                });

                UserCredViewCommand = new RelayCommand(o =>
                {
                    CurrentView = UsVM;
                });



            }
            catch (Exception ex)
            {
                LastError += "Error 1100 :" + ex.InnerException.Message;
            }

        }

    }

}
