using HomeAssistant.Helper;
using HomeAssistant.Helper.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class LoginViewModel : ThemedViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event LoginSuccessEventHandler LoginSuccess;

        public Command LoginRequestCommand { get; }

        private bool userAuthenticated;

        public bool UserAuthenticated
        {
            get
            {
                return userAuthenticated;
            }
            private set
            {
                userAuthenticated = value;
                NotifyPropertyChanged(nameof(UserAuthenticated));
            }
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public LoginViewModel(HomeAssistantClient apiClient)
        {
            LoginRequestCommand = new Command(async () => {

                if (string.IsNullOrEmpty(Username))
                {
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    return;
                }

                var userData = await apiClient.RequestLogin(Username, Password);

                if (userData is null)
                {
                    await Application.Current.MainPage.DisplayAlert("Login failed.", "Provided credentials are invalid.", "OK");
                    return;
                }

                LoginSuccess?.Invoke(this, new LoginSuccessEventArgs(userData));
                UserAuthenticated = true;
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
