using HomeAssistant.Helper;
using HomeAssistant.Helper.Events;
using System;
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
            LoginRequestCommand = new Command(async () => {

                if (string.IsNullOrEmpty(Username))
                {
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    return;
                }

                var userData = await HomeAssistantClient.RequestLogin(Username, Password);

                if (!userData)
                {
                    return;
                }

                UserAuthenticated = true;
            });

            Task.Run(async () => {
                var userData = await HomeAssistantClient.GetUserData();

                if (userData == null)
                {
                    return;
                }

                UserAuthenticated = true;
            });
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
