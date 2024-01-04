using App2.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace App2.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public Command LoginCommand {  get; set; }

        private string email;
        public string Email { get
            { 
                return email; 
            } 
            set 
            {
                email = value;
                OnPropertyChanged("EntriesHaveText");
             } 
        }
        private string password;    
        public string Password { get
            { 
                return password;
            }
            set
            { 
                password = value;
                OnPropertyChanged("EntriesHaveText");
            } 
        }
        private bool entriesHaveText;
        public bool EntriesHaveText
        {
            get {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            }
        }

        public MainVM()
        {
            LoginCommand = new Command<bool>(Login, CanLogin);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool CanLogin(bool param)
        {
            return EntriesHaveText;
        }

        private async void Login(bool param)
        {
             bool result = await Auth.LoginUser(Email, Password);
             if (result)
             {
                 await App.Current.MainPage.Navigation.PushAsync(new HomePage());
             }
        }

        private void OnPropertyChanged(string propertyName) {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
