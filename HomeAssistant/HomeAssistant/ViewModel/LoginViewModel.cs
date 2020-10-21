using HomeAssistant.Helper;
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
        public Command LoginRequestCommand { get; }

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

                HttpStatusCode status = await HomeAssistantHttpClient.RequestLogin(Username, Password);

                switch (status)
                {
                    case HttpStatusCode.OK:
                        await NavigationService.Navigation.NavigateToAsync<HomeViewModel>();
                        return;
                    case HttpStatusCode.RequestTimeout:
                        await Application.Current.MainPage.DisplayAlert("Connection error.", "Connection timed out.", "OK");
                        return;
                    case HttpStatusCode.Unauthorized:
                        await Application.Current.MainPage.DisplayAlert("Authorization error.", "Provided credentials are invalid.", "OK");
                        return;
                    case HttpStatusCode.ServiceUnavailable:
                        await Application.Current.MainPage.DisplayAlert("Service unavailable.", "Server is temporarily down. Try again later.", "OK");
                        return;
                    default:
                        await Application.Current.MainPage.DisplayAlert("Unknown error.", "Unknown error occured.", "OK");
                        return;
                }
            });
        }
    }
}
