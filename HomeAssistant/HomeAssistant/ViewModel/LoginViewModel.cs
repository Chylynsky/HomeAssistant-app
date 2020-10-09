using HomeAssistant.Helper;
using HomeAssistant.Helper.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HomeAssistant.ViewModel
{
    public class LoginViewModel : ThemedViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public event LoginSuccessEventHandler LoginSuccess;

        private HomeAssistantClient httpClient;

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

        public LoginViewModel()
        {
            httpClient = new HomeAssistantClient(new Uri("http://home.as"), new WebProxy("http://192.168.0.109:80"));

            LoginRequestCommand = new Command(async () => {

                if (string.IsNullOrEmpty(Username))
                {
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    return;
                }

                var userData = await httpClient.RequestLogin(Username, Password);

                if (userData is null)
                {
                    await Application.Current.MainPage.DisplayAlert("Authentication failed.", "Provided credentials are invalid.", "OK");
                    return;
                }

                LoginSuccess?.Invoke(this, new LoginSuccessEventArgs(userData));
                UserAuthenticated = true;
            });

            Task.Run(async () => {
                var userData = await httpClient.GetUserData();

                if (userData == null)
                {
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
